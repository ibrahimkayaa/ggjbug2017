// Features to consider in the future:
// - "Slowed" effect, e.g. look frosty, when slowed.
// - "Charged" effect, e.g. when buffed up.
// - Outline selection shader, maybe.

Shader "Aeon/Standard Hero" 
{
	Properties
	{
		_Color("Tint Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_PlayerColor("Player Color", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,0.8)
		_Outline("Outline width", Range(0.0, 0.1)) = .002
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
	}

	SubShader
	{
		//LOD 100
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

		// Pass 1: Player color + toon lighting
		// ==============================================================================================
		CGPROGRAM

		#pragma surface surf ToonRamp

		// Variables
		sampler2D _MainTex;
		fixed4 _Color;
		fixed4 _PlayerColor;
		sampler2D _Ramp;

		// From Unity ToonLit:
		// - custom lighting function that uses a texture ramp based on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif

			half d = dot(s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}

		struct Input {
			float2 uv_MainTex : TEXCOORD0;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			// ===============================================================================================
			// ============================================ INPUT ============================================
			// ===============================================================================================

			// Current Color
			// ----------------------------------------
			// Grab the current color from the texture
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

			// Identify Regions
			// ----------------------------------------
			bool isTransparentRegion = c.a < 0.2; // To support black mask: || (c.r == 0 && c.g == 0 && c.b == 0);
			bool isColoredRegion = c.a > 0.2 && c.a < 0.6;

			// ===============================================================================================
			// ========================================== CALCULATE ==========================================
			// ===============================================================================================

			// Transparent Region
			// ----------------------------------------
			// --> Clip
			clip(isTransparentRegion ? -1 : 1);

			// Colored Region
			// ----------------------------------------
			// --> Fill with Player Color
			if (isColoredRegion) {
				c.rgb = c.rgb * _PlayerColor.rgb * 3;
				c.a = 1;
			}
			else {
				c.rgb = c.rgb * _Color;
			}

			// ===============================================================================================
			// ============================================ APPLY ============================================
			// ===============================================================================================

			// Albedo & Alpha
			// ----------------------------------------
			o.Albedo = c.rgb;
			o.Alpha = c.a * _Color.a;
		}
		ENDCG
		
		// Pass 2: Outline inking
		// ==============================================================================================
		UsePass "Toon/Basic Outline/OUTLINE"
	}
	Fallback "Diffuse"
}
