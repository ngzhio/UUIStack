// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UIGray" {
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
	}
	
	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			ZTest [unity_GUIZTestMode]
	        Blend SrcAlpha OneMinusSrcAlpha  
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
                fixed gray : TEXCOORD1; 
			};
	 
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);    
				o.color = v.color;   
                o.gray = dot(v.color, fixed4(1,1,1,0));   
                return o;   
			}
				
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);   

                if (i.gray == 0)   
                {    
                    col.rgb = dot(col.rgb, fixed3(.299,.587,.114));    
                }else
				{
				    col = col*i.color;
				} 
  
                col.a = i.color.a * col.a;  

                return col;   
			}
			ENDCG
		}
	}
}
