using Silk.NET.OpenGL;
using System.Numerics;

namespace LogicGatesSandbox.Engine.Rendering;

public static class TextureRenderer
{
    private static readonly GL _gl = Engine.GL;

    private static uint _vao;
    private static uint _vbo;
    private static uint _ebo;
    private static uint _shaderProgram;

    private static int _viewProjectionLocation;
    private static int _modelLocation;
    private static int _colorLocation;

    public static void Initialize()
    {
        CreateShader();
        CreateQuad();

        _viewProjectionLocation = _gl.GetUniformLocation(
            _shaderProgram,
            "uViewProjection"
        );

        _modelLocation = _gl.GetUniformLocation(
            _shaderProgram,
            "uModel"
        );

        _colorLocation = _gl.GetUniformLocation(
            _shaderProgram,
            "uColor"
        );

        _gl.UseProgram(_shaderProgram);

        int textureLocation = _gl.GetUniformLocation(
            _shaderProgram,
            "uTexture"
        );

        _gl.Uniform1(textureLocation, 0);
    }

    public static void RenderTexture(
        Texture texture,
        Vector2 position,
        Vector2 scale,
        float rotation,
        Vector4 color
    )
    {
        Matrix4x4 viewProjection =
            Camera2D.GetViewProjectionMatrix();

        Matrix4x4 model =
            Matrix4x4.CreateScale(
                scale.X,
                scale.Y,
                1.0f
            )
            *
            Matrix4x4.CreateRotationZ(
                rotation
            )
            *
            Matrix4x4.CreateTranslation(
                position.X,
                position.Y,
                0.0f
            );

        _gl.UseProgram(_shaderProgram);

        _gl.UniformMatrix4(
            _viewProjectionLocation,
            1,
            false,
            ToArray(viewProjection)
        );

        _gl.UniformMatrix4(
            _modelLocation,
            1,
            false,
            ToArray(model)
        );

        _gl.Uniform4(
            _colorLocation,
            color
        );

        _gl.BindVertexArray(_vao);

        _gl.ActiveTexture(TextureUnit.Texture0);

        _gl.BindTexture(
            TextureTarget.Texture2D,
            texture.Id
        );

        unsafe
        {
            _gl.DrawElements(
                PrimitiveType.Triangles,
                6,
                DrawElementsType.UnsignedInt,
                null
            );
        }
    }

    private static unsafe void CreateQuad()
    {
        float[] vertices =
        [
            // Position       // UV
            -0.5f, -0.5f,      0.0f, 1.0f,
             0.5f, -0.5f,      1.0f, 1.0f,
             0.5f,  0.5f,      1.0f, 0.0f,
            -0.5f,  0.5f,      0.0f, 0.0f
        ];

        uint[] indices =
        [
            0, 1, 2,
            2, 3, 0
        ];

        _vao = _gl.GenVertexArray();
        _vbo = _gl.GenBuffer();
        _ebo = _gl.GenBuffer();

        _gl.BindVertexArray(_vao);

        _gl.BindBuffer(
            BufferTargetARB.ArrayBuffer,
            _vbo
        );

        fixed (float* data = vertices)
        {
            _gl.BufferData(
                BufferTargetARB.ArrayBuffer,
                (nuint)(vertices.Length * sizeof(float)),
                data,
                BufferUsageARB.StaticDraw
            );
        }

        _gl.BindBuffer(
            BufferTargetARB.ElementArrayBuffer,
            _ebo
        );

        fixed (uint* data = indices)
        {
            _gl.BufferData(
                BufferTargetARB.ElementArrayBuffer,
                (nuint)(indices.Length * sizeof(uint)),
                data,
                BufferUsageARB.StaticDraw
            );
        }

        _gl.VertexAttribPointer(
            0,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            (void*)0
        );

        _gl.EnableVertexAttribArray(0);

        _gl.VertexAttribPointer(
            1,
            2,
            VertexAttribPointerType.Float,
            false,
            4 * sizeof(float),
            (void*)(2 * sizeof(float))
        );

        _gl.EnableVertexAttribArray(1);

        _gl.BindVertexArray(0);
    }

    private static void CreateShader()
    {
        const string vertexShaderSource = """
            #version 330 core

            layout (location = 0) in vec2 aPosition;
            layout (location = 1) in vec2 aTexCoord;

            uniform mat4 uViewProjection;
            uniform mat4 uModel;

            out vec2 TexCoord;

            void main()
            {
                gl_Position =
                    uViewProjection *
                    uModel *
                    vec4(aPosition, 0.0, 1.0);

                TexCoord = aTexCoord;
            }
            """;

        const string fragmentShaderSource = """
            #version 330 core

            in vec2 TexCoord;

            uniform sampler2D uTexture;
            uniform vec4 uColor;

            out vec4 FragColor;

            void main()
            {
                FragColor =
                    texture(uTexture, TexCoord)
                    * uColor;
            }
            """;

        uint vertexShader = _gl.CreateShader(
            ShaderType.VertexShader
        );

        _gl.ShaderSource(
            vertexShader,
            vertexShaderSource
        );

        _gl.CompileShader(vertexShader);

        uint fragmentShader = _gl.CreateShader(
            ShaderType.FragmentShader
        );

        _gl.ShaderSource(
            fragmentShader,
            fragmentShaderSource
        );

        _gl.CompileShader(fragmentShader);

        _shaderProgram = _gl.CreateProgram();

        _gl.AttachShader(
            _shaderProgram,
            vertexShader
        );

        _gl.AttachShader(
            _shaderProgram,
            fragmentShader
        );

        _gl.LinkProgram(_shaderProgram);

        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);
    }

    private static float[] ToArray(Matrix4x4 matrix)
    {
        return
        [
            matrix.M11, matrix.M12, matrix.M13, matrix.M14,
            matrix.M21, matrix.M22, matrix.M23, matrix.M24,
            matrix.M31, matrix.M32, matrix.M33, matrix.M34,
            matrix.M41, matrix.M42, matrix.M43, matrix.M44
        ];
    }
}