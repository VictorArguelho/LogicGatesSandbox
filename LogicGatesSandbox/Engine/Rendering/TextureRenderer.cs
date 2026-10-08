using LogicGatesSandbox.Engine.Components;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public static class TextureRenderer
{
    private static readonly GL _gl = Engine.GL;

    private const int MAX_SPRITES_PER_BATCH = 10_000;

    private const int FLOATS_PER_INSTANCE = 9;

    private const int INSTANCE_SIZE =
        FLOATS_PER_INSTANCE * sizeof(float);

    private const int INDICES_PER_SPRITE = 6;

    private static uint _vao;
    private static uint _vbo;
    private static uint _ebo;
    private static uint _instanceVbo;

    private static uint _shaderProgram;

    private static int _viewProjectionLocation;

    private static readonly float[] _instanceBuffer =
        new float[
            MAX_SPRITES_PER_BATCH *
            FLOATS_PER_INSTANCE
        ];

    private static uint _boundTexture;

    public static void Initialize()
    {
        CreateShader();
        CreateBuffers();

        _viewProjectionLocation =
            _gl.GetUniformLocation(
                _shaderProgram,
                "uViewProjection"
            );

        _gl.UseProgram(
            _shaderProgram
        );

        int textureLocation =
            _gl.GetUniformLocation(
                _shaderProgram,
                "uTexture"
            );

        _gl.Uniform1(
            textureLocation,
            0
        );
    }

    public static void BeginFrame()
    {
        _gl.UseProgram(
            _shaderProgram
        );

        _gl.BindVertexArray(
            _vao
        );

        _gl.ActiveTexture(
            TextureUnit.Texture0
        );

        _boundTexture = 0;

        SetViewProjection(
            Camera.GetViewProjectionMatrix()
        );
    }

    public static void RenderBatch(
        List<RenderConfig> renderQueue,
        int startIndex,
        int count,
        Resources.Texture texture
    )
    {
        if (count <= 0)
            return;

        int currentIndex = startIndex;
        int remaining = count;

        while (remaining > 0)
        {
            int batchSize = Math.Min(
                remaining,
                MAX_SPRITES_PER_BATCH
            );

            BuildInstanceBuffer(
                renderQueue,
                currentIndex,
                batchSize
            );

            UploadInstanceBuffer(
                batchSize
            );

            BindTexture(
                texture
            );

            DrawInstanced(
                batchSize
            );

            currentIndex += batchSize;
            remaining -= batchSize;
        }
    }

    private static void BuildInstanceBuffer(
        List<RenderConfig> renderQueue,
        int startIndex,
        int count
    )
    {
        int instanceIndex = 0;

        int endIndex =
            startIndex + count;

        for (
            int i = startIndex;
            i < endIndex;
            i++
        )
        {
            RenderConfig config =
                renderQueue[i];

            _instanceBuffer[instanceIndex++] =
                config.Position.X;

            _instanceBuffer[instanceIndex++] =
                config.Position.Y;

            _instanceBuffer[instanceIndex++] =
                config.Size.X;

            _instanceBuffer[instanceIndex++] =
                config.Size.Y;

            _instanceBuffer[instanceIndex++] =
                config.Rotation;

            _instanceBuffer[instanceIndex++] =
                config.Color.X;

            _instanceBuffer[instanceIndex++] =
                config.Color.Y;

            _instanceBuffer[instanceIndex++] =
                config.Color.Z;

            _instanceBuffer[instanceIndex++] =
                config.Color.W;
        }
    }

    private static unsafe void UploadInstanceBuffer(
        int spriteCount
    )
    {
        int floatCount =
            spriteCount *
            FLOATS_PER_INSTANCE;

        _gl.BindBuffer(
            BufferTargetARB.ArrayBuffer,
            _instanceVbo
        );

        fixed (float* data = _instanceBuffer)
        {
            _gl.BufferSubData(
                BufferTargetARB.ArrayBuffer,
                0,
                (nuint)(
                    floatCount *
                    sizeof(float)
                ),
                data
            );
        }
    }

    private static void BindTexture(
        Resources.Texture texture
    )
    {
        if (_boundTexture == texture.Id)
            return;

        _gl.BindTexture(
            TextureTarget.Texture2D,
            texture.Id
        );

        _boundTexture = texture.Id;
    }

    private static unsafe void DrawInstanced(
        int spriteCount
    )
    {
        _gl.DrawElementsInstanced(
            PrimitiveType.Triangles,
            6,
            DrawElementsType.UnsignedInt,
            null,
            (uint)spriteCount
        );
    }

    private static void SetViewProjection(
        Matrix4x4 matrix
    )
    {
        float[] matrixBuffer =
        [
            matrix.M11,
            matrix.M12,
            matrix.M13,
            matrix.M14,

            matrix.M21,
            matrix.M22,
            matrix.M23,
            matrix.M24,

            matrix.M31,
            matrix.M32,
            matrix.M33,
            matrix.M34,

            matrix.M41,
            matrix.M42,
            matrix.M43,
            matrix.M44
        ];

        _gl.UniformMatrix4(
            _viewProjectionLocation,
            1,
            false,
            matrixBuffer
        );
    }

    private static unsafe void CreateBuffers()
    {
        _vao =
            _gl.GenVertexArray();

        _vbo =
            _gl.GenBuffer();

        _ebo =
            _gl.GenBuffer();

        _instanceVbo =
            _gl.GenBuffer();

        _gl.BindVertexArray(
            _vao
        );

        CreateQuad();

        CreateIndexBuffer();

        CreateInstanceBuffer();

        _gl.BindVertexArray(0);
    }

    private static unsafe void CreateQuad()
    {
        float[] vertices =
        [
            -0.5f, -0.5f, 0.0f, 1.0f,
             0.5f, -0.5f, 1.0f, 1.0f,
             0.5f,  0.5f, 1.0f, 0.0f,
            -0.5f,  0.5f, 0.0f, 0.0f
        ];

        _gl.BindBuffer(
            BufferTargetARB.ArrayBuffer,
            _vbo
        );

        fixed (float* data = vertices)
        {
            _gl.BufferData(
                BufferTargetARB.ArrayBuffer,
                (nuint)(
                    vertices.Length *
                    sizeof(float)
                ),
                data,
                BufferUsageARB.StaticDraw
            );
        }

        int stride =
            4 * sizeof(float);

        _gl.VertexAttribPointer(
            0,
            2,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)0
        );

        _gl.EnableVertexAttribArray(0);

        _gl.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)(2 * sizeof(float))
        );

        _gl.EnableVertexAttribArray(1);
    }

    private static unsafe void CreateIndexBuffer()
    {
        uint[] indices =
        [
            0, 1, 2,
            2, 3, 0
        ];

        _gl.BindBuffer(
            BufferTargetARB.ElementArrayBuffer,
            _ebo
        );

        fixed (uint* data = indices)
        {
            _gl.BufferData(
                BufferTargetARB.ElementArrayBuffer,
                (nuint)(
                    indices.Length *
                    sizeof(uint)
                ),
                data,
                BufferUsageARB.StaticDraw
            );
        }
    }

    private static unsafe void CreateInstanceBuffer()
    {
        _gl.BindBuffer(
            BufferTargetARB.ArrayBuffer,
            _instanceVbo
        );

        _gl.BufferData(
            BufferTargetARB.ArrayBuffer,
            (nuint)(
                _instanceBuffer.Length *
                sizeof(float)
            ),
            null,
            BufferUsageARB.StreamDraw
        );

        int stride =
            INSTANCE_SIZE;

        // Position
        _gl.VertexAttribPointer(
            2,
            2,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)0
        );

        _gl.EnableVertexAttribArray(2);

        _gl.VertexAttribDivisor(
            2,
            1
        );

        // Scale
        _gl.VertexAttribPointer(
            3,
            2,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)(2 * sizeof(float))
        );

        _gl.EnableVertexAttribArray(3);

        _gl.VertexAttribDivisor(
            3,
            1
        );

        // Rotation
        _gl.VertexAttribPointer(
            4,
            1,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)(4 * sizeof(float))
        );

        _gl.EnableVertexAttribArray(4);

        _gl.VertexAttribDivisor(
            4,
            1
        );

        // Color
        _gl.VertexAttribPointer(
            5,
            4,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)(5 * sizeof(float))
        );

        _gl.EnableVertexAttribArray(5);

        _gl.VertexAttribDivisor(
            5,
            1
        );
    }

    private static void CreateShader()
    {
        const string vertexShaderSource = """
            #version 330 core

            layout (location = 0) in vec2 aPosition;
            layout (location = 1) in vec2 aTexCoord;

            layout (location = 2) in vec2 aInstancePosition;
            layout (location = 3) in vec2 aInstanceScale;
            layout (location = 4) in float aInstanceRotation;
            layout (location = 5) in vec4 aInstanceColor;

            uniform mat4 uViewProjection;

            out vec2 TexCoord;
            out vec4 Color;

            void main()
            {
                float cosRotation =
                    cos(aInstanceRotation);

                float sinRotation =
                    sin(aInstanceRotation);

                mat2 rotation =
                    mat2(
                        cosRotation,
                        -sinRotation,
                        sinRotation,
                        cosRotation
                    );

                vec2 scaledPosition =
                    aPosition *
                    aInstanceScale;

                vec2 rotatedPosition =
                    rotation *
                    scaledPosition;

                vec2 worldPosition =
                    rotatedPosition +
                    aInstancePosition;

                gl_Position =
                    uViewProjection *
                    vec4(
                        worldPosition,
                        0.0,
                        1.0
                    );

                TexCoord = aTexCoord;
                Color = aInstanceColor;
            }
            """;

        const string fragmentShaderSource = """
            #version 330 core

            in vec2 TexCoord;
            in vec4 Color;

            uniform sampler2D uTexture;

            out vec4 FragColor;

            void main()
            {
                FragColor =
                    texture(
                        uTexture,
                        TexCoord
                    )
                    *
                    Color;
            }
            """;

        uint vertexShader =
            _gl.CreateShader(
                ShaderType.VertexShader
            );

        _gl.ShaderSource(
            vertexShader,
            vertexShaderSource
        );

        _gl.CompileShader(
            vertexShader
        );

        uint fragmentShader =
            _gl.CreateShader(
                ShaderType.FragmentShader
            );

        _gl.ShaderSource(
            fragmentShader,
            fragmentShaderSource
        );

        _gl.CompileShader(
            fragmentShader
        );

        _shaderProgram =
            _gl.CreateProgram();

        _gl.AttachShader(
            _shaderProgram,
            vertexShader
        );

        _gl.AttachShader(
            _shaderProgram,
            fragmentShader
        );

        _gl.LinkProgram(
            _shaderProgram
        );

        _gl.DeleteShader(
            vertexShader
        );

        _gl.DeleteShader(
            fragmentShader
        );
    }
}