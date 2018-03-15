Shader "Hidden/Zololgo/Post/Sharpen" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct appdata {
				half4 vertex : POSITION;
				half2 uv : TEXCOORD0;
			};
			struct v2f {
				half2 uv : TEXCOORD0;
				half4 vertex : SV_POSITION;
			};
			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			sampler2D _MainTex;
			half4 _MainTex_TexelSize;
			half _Sharpness;
			half4 frag (v2f i) : SV_Target {
				half2 ts = _MainTex_TexelSize * 0.5;
				half2 uv01 = i.uv-ts.xy;
				half2 uv02 = i.uv+ts.xy;
				half2 uv03 = i.uv+ts.xy*half4(-1,1,1,1);
				half2 uv04 = i.uv+ts.xy*half4(1,-1,1,1);			
				half4 lum = half4(0.35,0.55,0.1,1.0);
				half4 t = tex2D(_MainTex, i.uv);
				half4 s = half4( dot(tex2D(_MainTex, uv01), lum), dot(tex2D(_MainTex, uv02), lum), dot(tex2D(_MainTex, uv03), lum),dot(tex2D(_MainTex, uv04), lum));
				half4 d = dot(t,lum) - dot(s, half4(0.25, 0.25, 0.25, 0.25));
				return _Sharpness * d + t;
			}
			ENDCG
		}
	}
}
