// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Custom/Shear"
{
    Properties
    {
        // Shear(シア変換)用のプロパティ: 平行四辺形の変形に用いる
        [Header(Shear)]
        [Toggle(USE_SHEAR_DEGREE)] _UseShearDegree ("Use ShearDegree", float ) = 0 // 角度数に切り替える
        _ShearDegree ("ShearDegree", Range(-90,90)) = 0 // 角度数の調整値
        _ShearXY ("ShearXY", Vector) = (0,0,0,0)        // ただのXY軸の調整値
        
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert_Custom // シア変形を追加した関数に変更
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #pragma multi_compile _ USE_SHEAR_DEGREE // 角度数指定切り替えのプリプロセッサ命令定義
            #include "UnitySprites.cginc"

            // Shear(シア変換)の変数
            float _ShearDegree;
            vector _ShearXY;

            // Sprite/Default UnitySprite.cgincを拡張
            v2f SpriteVert_Custom(appdata_t IN)
            {
                // シア変換
                #ifdef USE_SHEAR_DEGREE
                float shearRadians = radians(_ShearDegree);
                _ShearXY.xy = float2(sin(shearRadians), 0);
                #endif
                IN.vertex.xy += IN.vertex.yx * _ShearXY;
                
                v2f OUT;

                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color * _RendererColor;

                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }
        ENDCG
        }
    }
}
