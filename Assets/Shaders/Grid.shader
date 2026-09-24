Shader "Custom/Grid"
{
    Properties
    {
        _LineColor ("Line Color", Color) = (0.2, 0.2, 0.2, 1)
        _BackgroundColor ("Background Color", Color) = (0.1, 0.1, 0.1, 1)

        [Space]

        _CellSize ("Cell Size", Float) = 1.0
        _LineWidth ("Line Width (Pixels)", Float) = 1.0

        [Space]

        _GridChangeStart ("Grid Change Start Zoom", Float) = 25.0
        _GridZoomStep ("Zoom Per Grid Change", Float) = 2.0
        _GridScaleStep ("Grid Scale Multiplier", Float) = 2.0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Background"
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _LineColor;
            fixed4 _BackgroundColor;

            float _CellSize;
            float _LineWidth;

            float _GridChangeStart;
            float _GridZoomStep;
            float _GridScaleStep;

            float4 _CameraPosition;
            float _CameraZoom;
            float _CameraAspect;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float GetGridScale(float zoom)
            {
                if (zoom < _GridChangeStart)
                    return 1.0;

                float steps =
                    floor(
                        log(
                            zoom / _GridChangeStart
                        ) /
                        log(_GridZoomStep)
                    ) + 1.0;

                return pow(
                    _GridScaleStep,
                    steps
                );
            }

            v2f vert(appdata input)
            {
                v2f result;

                result.vertex =
                    UnityObjectToClipPos(input.vertex);

                result.uv =
                    input.uv;

                return result;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                float worldHeight =
                    _CameraZoom * 2.0;

                float worldWidth =
                    worldHeight * _CameraAspect;

                float2 worldPos =
                    _CameraPosition.xy +
                    (input.uv - 0.5) *
                    float2(
                        worldWidth,
                        worldHeight
                    );

                float gridScale =
                    GetGridScale(_CameraZoom);

                float cellSize =
                    _CellSize *
                    gridScale;

                float2 gridCoord =
                    worldPos /
                    cellSize;

                float2 grid =
                    abs(
                        frac(gridCoord) -
                        0.5
                    );

                float2 derivative =
                    fwidth(gridCoord);

                float2 lineWidth =
                    derivative *
                    _LineWidth;

                float lineX =
                    1.0 -
                    smoothstep(
                        0.0,
                        lineWidth.x,
                        grid.x
                    );

                float lineY =
                    1.0 -
                    smoothstep(
                        0.0,
                        lineWidth.y,
                        grid.y
                    );

                float gridMask =
                    max(
                        lineX,
                        lineY
                    );

                return lerp(
                    _BackgroundColor,
                    _LineColor,
                    gridMask
                );
            }

            ENDCG
        }
    }
}
