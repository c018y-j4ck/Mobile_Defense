//Maya ASCII 2024 scene
//Name: T5Pointer.ma
//Last modified: Fri, Apr 12, 2024 11:33:51 AM
//Codeset: 1252
requires maya "2024";
requires -nodeType "aiStandardSurface" "mtoa" "5.3.1.1";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2024";
fileInfo "version" "2024";
fileInfo "cutIdentifier" "202304191415-7fa20164c6";
fileInfo "osv" "Windows 10 Home v2009 (Build: 19045)";
fileInfo "UUID" "3C48CA36-4B8F-94F4-19A1-35BA39917148";
createNode transform -n "pSphere1";
	rename -uid "036BBAC9-478F-3291-B98B-E8BF2E28510D";
	setAttr ".s" -type "double3" 1 3.2501154178859926 1 ;
createNode transform -n "transform2" -p "pSphere1";
	rename -uid "315D9328-495C-33D7-3586-3BAFF6053AA5";
	setAttr ".v" no;
createNode mesh -n "pSphereShape1" -p "transform2";
	rename -uid "7DFB71DE-42A1-BA56-E69E-56819255CEEF";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50000005960464478 0.32500001788139343 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCylinder1";
	rename -uid "FC7A6D33-48A4-8239-8F74-C19F0F8A7A48";
	setAttr ".t" -type "double3" 0 -2.0672396196409712 0 ;
	setAttr ".r" -type "double3" 0 0 180 ;
	setAttr ".s" -type "double3" 0.71938820118222657 1.0878337062402814 0.71938820118222657 ;
createNode transform -n "transform1" -p "pCylinder1";
	rename -uid "3F4BBD78-49CF-00C9-F18B-488B89B3C294";
	setAttr ".v" no;
createNode mesh -n "pCylinderShape1" -p "transform1";
	rename -uid "4B4DD92E-48A5-4C8D-C341-DBB721425B96";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49999998509883881 0.84374997019767761 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 21 ".pt[41:61]" -type "float3"  -0.75541425 -1.2160954 0.24544878 
		-0.64259338 -1.2160954 0.46687153 8.2718061e-25 -1.2160954 -4.4384375e-08 -0.46687147 
		-1.2160954 0.64259338 -0.24544884 -1.2160954 0.75541401 3.469447e-18 -1.2160954 0.79428911 
		0.24544884 -1.2160954 0.75541395 0.46687147 -1.2160954 0.6425932 0.64259315 -1.2160954 
		0.46687135 0.75541377 -1.2160954 0.24544866 0.79428911 -1.2160954 -4.4384375e-08 
		0.75541377 -1.2160954 -0.24544877 0.64259315 -1.2160954 -0.46687141 0.46687147 -1.2160954 
		-0.64259315 0.2454486 -1.2160954 -0.75541371 2.4286129e-17 -1.2160954 -0.79428911 
		-0.2454486 -1.2160954 -0.75541371 -0.46687123 -1.2160954 -0.64259315 -0.64259315 
		-1.2160954 -0.46687135 -0.75541353 -1.2160954 -0.24544871 -0.79428911 -1.2160954 
		-4.4384375e-08;
createNode transform -n "pSphere2";
	rename -uid "4D426D0D-43DE-F674-FEAE-7A9547416A85";
	setAttr ".t" -type "double3" 0 0 0.74842693942728866 ;
	setAttr ".s" -type "double3" 0.49710610932788346 0.49710610932788346 0.49710610932788346 ;
createNode transform -n "transform5" -p "pSphere2";
	rename -uid "56CACD5D-4CC9-D944-835E-24863365E954";
	setAttr ".v" no;
createNode mesh -n "pSphereShape2" -p "transform5";
	rename -uid "E53E4AF6-4AE7-ABE2-D78A-63A46F8B7744";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "group";
	rename -uid "5EBBFA80-4F42-97A0-682C-4681B244DE9F";
	setAttr ".t" -type "double3" 0 0.96492307463184424 -0.10578922000977098 ;
	setAttr ".rp" -type "double3" -5.9259666118638421e-08 0 0.74842685053778946 ;
	setAttr ".sp" -type "double3" -5.9259666118638421e-08 0 0.74842685053778946 ;
createNode transform -n "pasted__pSphere2" -p "group";
	rename -uid "76609A3C-4FB3-27BC-E46A-C69279E4D8C2";
	setAttr ".t" -type "double3" 0 0 0.74842693942728866 ;
	setAttr ".s" -type "double3" 0.49710610932788346 0.49710610932788346 0.49710610932788346 ;
createNode transform -n "transform4" -p "|group|pasted__pSphere2";
	rename -uid "C72DB040-4FE7-0B34-20B3-AE9941606BBB";
	setAttr ".v" no;
createNode mesh -n "pasted__pSphereShape2" -p "transform4";
	rename -uid "D2A934CD-4BDA-BAE9-EDE0-ADAFD0CE30D9";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "group1";
	rename -uid "80708F95-4E9F-F1AE-1028-4699E1A0053B";
	setAttr ".t" -type "double3" 0 2.0222548709226564 -0.34422508481793779 ;
	setAttr ".rp" -type "double3" -5.9259666118638421e-08 0 0.74842685053778946 ;
	setAttr ".sp" -type "double3" -5.9259666118638421e-08 0 0.74842685053778946 ;
createNode transform -n "pasted__pSphere2" -p "group1";
	rename -uid "CB72F210-494E-97EB-6443-9DA0AA83C8AE";
	setAttr ".t" -type "double3" 0 0 0.74842693942728866 ;
	setAttr ".s" -type "double3" 0.49710610932788346 0.49710610932788346 0.49710610932788346 ;
createNode transform -n "transform3" -p "|group1|pasted__pSphere2";
	rename -uid "73C63F9B-40B7-BD0A-F9DA-4AB4A28D6F0F";
	setAttr ".v" no;
createNode mesh -n "pasted__pSphereShape2" -p "transform3";
	rename -uid "AB1BDC76-428B-9FB8-9FCA-5EBA6942B302";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pSphere3";
	rename -uid "2B0D41A4-4BEF-C896-7410-B6A69CD7729C";
	setAttr ".rp" -type "double3" 2.384185791015625e-07 -0.067184330692661165 0.24323952198028564 ;
	setAttr ".sp" -type "double3" 2.384185791015625e-07 -0.067184330692661165 0.24323952198028564 ;
createNode mesh -n "pSphere3Shape" -p "pSphere3";
	rename -uid "F7FA9C99-4AC3-553F-CCEA-CABB727D5AA7";
	setAttr -k off ".v";
	setAttr -s 6 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode groupParts -n "groupParts8";
	rename -uid "9A697539-430D-F81A-1EE3-2FB7A1D9DC22";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[1531:1610]";
createNode groupParts -n "groupParts7";
	rename -uid "7FD26059-43B4-E8A7-6E40-AC811E5D3251";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[1200:1530]";
createNode groupParts -n "groupParts6";
	rename -uid "59EAF4A6-4B9E-657E-A8B6-0DB83255C1A4";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:1199]";
createNode polyUnite -n "polyUnite1";
	rename -uid "BD654FD7-4A8A-4E1F-A7FA-D5B60B3D6E2C";
	setAttr -s 5 ".ip";
	setAttr -s 5 ".im";
createNode groupId -n "groupId1";
	rename -uid "CC8C1BF1-4DDD-7D5E-991C-CE9AC2B53793";
	setAttr ".ihi" 0;
createNode shadingEngine -n "aiStandardSurface1SG";
	rename -uid "8052C6FE-47AA-6B83-3CF2-57B34E5843F0";
	setAttr ".ihi" 0;
	setAttr -s 7 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 7 ".gn";
createNode materialInfo -n "materialInfo1";
	rename -uid "B84E7585-4DE9-2DDF-6BAD-54B367148803";
createNode aiStandardSurface -n "aiStandardSurface1";
	rename -uid "FC414018-4767-D5ED-71F4-4B91F5595889";
	setAttr ".base_color" -type "float3" 0 1 0.78939998 ;
	setAttr ".metalness" 0.12820513546466827;
createNode groupParts -n "groupParts1";
	rename -uid "5E64B959-45F7-6448-B645-CCA034A7EBD3";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:399]";
createNode polySphere -n "polySphere2";
	rename -uid "0669AD44-4144-2FEC-475B-27A69751C03F";
createNode groupId -n "groupId2";
	rename -uid "F77B6007-43E3-4759-BC51-60903E354796";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts2";
	rename -uid "71ABA5BA-4E6D-3F4D-881C-F6A32BBBC7DB";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:399]";
createNode polySphere -n "pasted__polySphere2";
	rename -uid "2CB1C6DD-4DCC-59DF-FE6D-F7863A0BEE8F";
createNode groupId -n "groupId3";
	rename -uid "6E57014F-4F0E-2FE8-4FA9-09A10B5ECB82";
	setAttr ".ihi" 0;
createNode groupId -n "groupId4";
	rename -uid "6726D82F-412B-8E3B-F6A5-2BABFF1F7673";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts3";
	rename -uid "A829B87F-4DA8-1164-1AD7-5F9D9D5FD226";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:399]";
createNode polySphere -n "pasted__polySphere3";
	rename -uid "AFA78461-478A-FF91-1C9F-4D883A7226C7";
createNode groupId -n "groupId5";
	rename -uid "A4F23051-45C2-FE2D-535F-4A909F832C5A";
	setAttr ".ihi" 0;
createNode groupId -n "groupId6";
	rename -uid "F09FAEDF-44AC-B183-50D8-52A357517DE6";
	setAttr ".ihi" 0;
createNode groupId -n "groupId7";
	rename -uid "E59CAC7F-4506-3019-F90C-DA8FFBD6DBF9";
	setAttr ".ihi" 0;
createNode shadingEngine -n "aiStandardSurface2SG";
	rename -uid "352490F5-4E32-F8D4-A6FD-C593EFF34172";
	setAttr ".ihi" 0;
	setAttr -s 3 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 3 ".gn";
createNode materialInfo -n "materialInfo2";
	rename -uid "6DA3B2E4-43FB-6FFB-AC76-FAAF3F5AB9B6";
createNode aiStandardSurface -n "aiStandardSurface2";
	rename -uid "847DD60C-4CEF-14F3-C709-A0ACB8DE7E5B";
	setAttr ".base_color" -type "float3" 1 0.0146 0 ;
	setAttr ".metalness" 0.69871795177459717;
createNode groupParts -n "groupParts4";
	rename -uid "4DE00781-4697-90F9-3EE6-4CAC57277E7C";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:330]";
createNode polyCloseBorder -n "polyCloseBorder1";
	rename -uid "400379EE-49E6-5430-FF04-77BE43259952";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode polyConnectComponents -n "polyConnectComponents1";
	rename -uid "407A7447-42F8-8878-BA14-E4B6A4AD5EDC";
	setAttr ".uopa" yes;
createNode deleteComponent -n "deleteComponent1";
	rename -uid "6ADD7416-4FFD-1DD9-0DC9-F78F399E3B58";
	setAttr ".dc" -type "componentList" 2 "f[0:79]" "f[360:379]";
createNode polyTweak -n "polyTweak3";
	rename -uid "39291EF5-4528-D0F6-5C3F-9180428DED38";
	setAttr ".uopa" yes;
	setAttr -s 38 ".tk";
	setAttr ".tk[376]" -type "float3" -0.049450155 -0.3327961 -0.31101915 ;
	setAttr ".tk[377]" -type "float3" 0.090911806 -0.3327961 -0.066050917 ;
	setAttr ".tk[378]" -type "float3" 0.0090950131 -0.44350305 -0.0066080689 ;
	setAttr ".tk[379]" -type "float3" -0.13863324 -0.44385499 -0.28131264 ;
	setAttr ".tk[380]" -type "float3" 0.28051603 -0.3327961 0.14313996 ;
	setAttr ".tk[381]" -type "float3" 0.22470379 -0.44385499 0.21877861 ;
	setAttr ".tk[382]" -type "float3" -0.054977056 -0.57260799 0.039943606 ;
	setAttr ".tk[383]" -type "float3" -0.2135191 -0.57287437 -0.25581461 ;
	setAttr ".tk[384]" -type "float3" 0.17731294 -0.57287437 0.2821202 ;
	setAttr ".tk[385]" -type "float3" -0.093968332 -0.70559555 0.068272322 ;
	setAttr ".tk[386]" -type "float3" -0.26610419 -0.70559555 -0.23829252 ;
	setAttr ".tk[387]" -type "float3" 0.14439906 -0.70559555 0.32671705 ;
	setAttr ".tk[388]" -type "float3" -0.28051519 -0.33279616 0.14313996 ;
	setAttr ".tk[389]" -type "float3" -0.090911925 -0.33279616 -0.066050917 ;
	setAttr ".tk[390]" -type "float3" -0.0090948306 -0.44350311 -0.0066081285 ;
	setAttr ".tk[391]" -type "float3" -0.22470368 -0.44385505 0.21877861 ;
	setAttr ".tk[392]" -type "float3" 0.049450167 -0.33279616 -0.31101915 ;
	setAttr ".tk[393]" -type "float3" 0.13863306 -0.44385511 -0.28131247 ;
	setAttr ".tk[394]" -type "float3" 0.054976825 -0.57260805 0.039943606 ;
	setAttr ".tk[395]" -type "float3" -0.17731285 -0.57287437 0.28212014 ;
	setAttr ".tk[396]" -type "float3" 0.21351862 -0.57287443 -0.25581455 ;
	setAttr ".tk[397]" -type "float3" 0.093968332 -0.70559555 0.068272203 ;
	setAttr ".tk[398]" -type "float3" -0.14439902 -0.70559549 0.32671693 ;
	setAttr ".tk[399]" -type "float3" 0.26610446 -0.70559561 -0.23829246 ;
	setAttr ".tk[400]" -type "float3" 0.28068614 -0.3327961 0.14280653 ;
	setAttr ".tk[401]" -type "float3" 1.2293458e-07 -0.3327961 0.11237273 ;
	setAttr ".tk[402]" -type "float3" 1.2293458e-07 -0.44350311 0.011242509 ;
	setAttr ".tk[403]" -type "float3" 0.30907327 -0.44385505 0.053194195 ;
	setAttr ".tk[404]" -type "float3" -0.2806854 -0.3327961 0.14280653 ;
	setAttr ".tk[405]" -type "float3" -0.30907252 -0.44385505 0.053194076 ;
	setAttr ".tk[406]" -type "float3" 6.3329935e-08 -0.57260799 -0.067955643 ;
	setAttr ".tk[407]" -type "float3" 0.33246207 -0.57287437 -0.022376478 ;
	setAttr ".tk[408]" -type "float3" -0.33246145 -0.57287437 -0.022376478 ;
	setAttr ".tk[409]" -type "float3" 3.7252903e-09 -0.70559555 -0.11615205 ;
	setAttr ".tk[410]" -type "float3" 0.34919494 -0.70559555 -0.075218454 ;
	setAttr ".tk[411]" -type "float3" -0.34919444 -0.70559555 -0.075218335 ;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "38B6DA7B-45BA-D5CE-25D2-FF92375606F2";
	setAttr ".ics" -type "componentList" 9 "f[80:81]" "f[86:87]" "f[93:94]" "f[100:101]" "f[106:107]" "f[113:114]" "f[120:121]" "f[126:127]" "f[133:134]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 3.2501154178859926 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 1.7881393e-07 -1.6512597 0.09081766 ;
	setAttr ".rs" 38413;
	setAttr ".lt" -type "double3" 2.7755575615628914e-17 -5.5511151231257827e-17 0.66490273604282069 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.90450876951217651 -2.2981786122944827 -0.76942133903503418 ;
	setAttr ".cbx" -type "double3" 0.90450912714004517 -1.004340827621474 0.95105665922164917 ;
createNode polyTweak -n "polyTweak1";
	rename -uid "1A77C452-41F6-2444-333C-C6AFE67020CA";
	setAttr ".uopa" yes;
	setAttr -s 22 ".tk";
	setAttr ".tk[360]" -type "float3" -0.11599794 0.059554581 0.03769001 ;
	setAttr ".tk[361]" -type "float3" -0.098673724 0.059554581 0.071690679 ;
	setAttr ".tk[362]" -type "float3" -0.071690679 0.059554581 0.098673716 ;
	setAttr ".tk[363]" -type "float3" -0.037690014 0.059554581 0.11599793 ;
	setAttr ".tk[364]" -type "float3" -2.1783744e-10 0.059554581 0.12196743 ;
	setAttr ".tk[365]" -type "float3" 0.03769001 0.059554581 0.11599791 ;
	setAttr ".tk[366]" -type "float3" 0.071690664 0.059554581 0.098673701 ;
	setAttr ".tk[367]" -type "float3" 0.098673701 0.059554581 0.071690656 ;
	setAttr ".tk[368]" -type "float3" 0.1159979 0.059554581 0.037689991 ;
	setAttr ".tk[369]" -type "float3" 0.12196743 0.059554581 -7.3701667e-09 ;
	setAttr ".tk[370]" -type "float3" 0.1159979 0.059554581 -0.03769001 ;
	setAttr ".tk[371]" -type "float3" 0.098673701 0.059554581 -0.071690649 ;
	setAttr ".tk[372]" -type "float3" 0.071690649 0.059554581 -0.098673701 ;
	setAttr ".tk[373]" -type "float3" 0.037690006 0.059554581 -0.1159979 ;
	setAttr ".tk[374]" -type "float3" 3.4170733e-09 0.059554581 -0.12196741 ;
	setAttr ".tk[375]" -type "float3" -0.037689991 0.059554581 -0.11599788 ;
	setAttr ".tk[376]" -type "float3" -0.071690634 0.059554581 -0.098673701 ;
	setAttr ".tk[377]" -type "float3" -0.098673701 0.059554581 -0.071690649 ;
	setAttr ".tk[378]" -type "float3" -0.11599787 0.059554581 -0.037689999 ;
	setAttr ".tk[379]" -type "float3" -0.12196738 0.059554581 -7.3701667e-09 ;
	setAttr ".tk[381]" -type "float3" -2.1783744e-10 0.049955372 -7.3701667e-09 ;
createNode polySphere -n "polySphere1";
	rename -uid "90AFA54F-407C-00AF-6A77-ED8D39174E59";
createNode groupId -n "groupId8";
	rename -uid "BF1C6D62-4F60-438C-5B7A-58990274273D";
	setAttr ".ihi" 0;
createNode groupId -n "groupId9";
	rename -uid "1E29291A-4C10-5BDD-9471-E7A705DEA9E1";
	setAttr ".ihi" 0;
createNode shadingEngine -n "aiStandardSurface3SG";
	rename -uid "DD561226-4C7B-8277-1048-74849841885B";
	setAttr ".ihi" 0;
	setAttr -s 3 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 3 ".gn";
createNode materialInfo -n "materialInfo3";
	rename -uid "01623548-4E0A-CDD7-4D96-9DA095FB42A9";
createNode aiStandardSurface -n "aiStandardSurface3";
	rename -uid "69F7B802-4356-8B1C-4BC0-63B66E372D20";
	setAttr ".base_color" -type "float3" 0 0 0 ;
	setAttr ".metalness" 0.82051283121109009;
createNode groupParts -n "groupParts5";
	rename -uid "8BC35595-4420-83BD-8984-7C985F8AFEDA";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:79]";
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "4F3756F6-4F24-984B-7A0A-79A228735885";
	setAttr ".ics" -type "componentList" 1 "f[40:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 2.6738269965717918 -0.31614318951797948 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 2.6738269 0.68385679 -1.7881393e-07 ;
	setAttr ".rs" 42633;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" 1.6738267581532127 0.68385681048202052 -1.0000004768371582 ;
	setAttr ".cbx" -type "double3" 3.6738269965717918 0.68385681048202052 1.0000001192092896 ;
createNode polyTweak -n "polyTweak2";
	rename -uid "11811D6E-4C0E-9A6E-D866-8D9AC485D4BA";
	setAttr ".uopa" yes;
	setAttr -s 22 ".tk";
	setAttr ".tk[0]" -type "float3" -0.56304002 0 0.18294325 ;
	setAttr ".tk[1]" -type "float3" -0.47894949 0 0.34797871 ;
	setAttr ".tk[2]" -type "float3" -0.34797782 0 0.47894937 ;
	setAttr ".tk[3]" -type "float3" -0.18294245 0 0.56303996 ;
	setAttr ".tk[4]" -type "float3" 6.836836e-09 0 0.59201384 ;
	setAttr ".tk[5]" -type "float3" 0.18294331 0 0.56303996 ;
	setAttr ".tk[6]" -type "float3" 0.34797847 0 0.47895086 ;
	setAttr ".tk[7]" -type "float3" 0.47895086 0 0.34797758 ;
	setAttr ".tk[8]" -type "float3" 0.56303996 0 0.18294314 ;
	setAttr ".tk[9]" -type "float3" 0.59201533 0 -3.9697753e-08 ;
	setAttr ".tk[10]" -type "float3" 0.56303996 0 -0.18294245 ;
	setAttr ".tk[11]" -type "float3" 0.47894919 0 -0.34797859 ;
	setAttr ".tk[12]" -type "float3" 0.34797835 0 -0.47894919 ;
	setAttr ".tk[13]" -type "float3" 0.18294239 0 -0.56303996 ;
	setAttr ".tk[14]" -type "float3" 2.448035e-08 0 -0.5920136 ;
	setAttr ".tk[15]" -type "float3" -0.18294227 0 -0.56303996 ;
	setAttr ".tk[16]" -type "float3" -0.34797746 0 -0.47895074 ;
	setAttr ".tk[17]" -type "float3" -0.47895062 0 -0.34797835 ;
	setAttr ".tk[18]" -type "float3" -0.56304157 0 -0.18294239 ;
	setAttr ".tk[19]" -type "float3" -0.59201348 0 -3.9697753e-08 ;
	setAttr ".tk[40]" -type "float3" 6.836836e-09 0 -3.9697753e-08 ;
createNode polyCylinder -n "polyCylinder1";
	rename -uid "733A0176-485F-3C15-F1B0-98B16A8A7088";
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode groupId -n "groupId10";
	rename -uid "96FD62E5-45E1-DCE1-F741-CC97CF64B058";
	setAttr ".ihi" 0;
createNode groupId -n "groupId11";
	rename -uid "F57A5B9D-4765-FCB4-0392-19AF1CD6D5D6";
	setAttr ".ihi" 0;
createNode groupId -n "groupId12";
	rename -uid "1D0AAE64-4637-6FB1-4D81-4EBACF276AE5";
	setAttr ".ihi" 0;
createNode groupId -n "groupId13";
	rename -uid "3BDA9795-4075-7967-A659-8EBF4789FA00";
	setAttr ".ihi" 0;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "E2F463BB-48D2-57E3-B442-819A770F8BE5";
	setAttr -s 5 ".lnk";
	setAttr -s 5 ".slnk";
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
	setAttr ".rtfm" 1;
select -ne :renderPartition;
	setAttr -s 5 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 8 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :standardSurface1;
	setAttr ".bc" -type "float3" 0.40000001 0.40000001 0.40000001 ;
	setAttr ".sr" 0.5;
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr ".ren" -type "string" "arnold";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :defaultColorMgtGlobals;
	setAttr ".cfe" yes;
	setAttr ".cfp" -type "string" "<MAYA_RESOURCES>/OCIO-configs/Maya2022-default/config.ocio";
	setAttr ".vtn" -type "string" "ACES 1.0 SDR-video (sRGB)";
	setAttr ".vn" -type "string" "ACES 1.0 SDR-video";
	setAttr ".dn" -type "string" "sRGB";
	setAttr ".wsn" -type "string" "ACEScg";
	setAttr ".otn" -type "string" "ACES 1.0 SDR-video (sRGB)";
	setAttr ".potn" -type "string" "ACES 1.0 SDR-video (sRGB)";
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
connectAttr "groupId7.id" "pSphereShape1.iog.og[0].gid";
connectAttr "aiStandardSurface2SG.mwc" "pSphereShape1.iog.og[0].gco";
connectAttr "groupParts4.og" "pSphereShape1.i";
connectAttr "groupId8.id" "pSphereShape1.ciog.cog[0].cgid";
connectAttr "groupId9.id" "pCylinderShape1.iog.og[0].gid";
connectAttr "aiStandardSurface3SG.mwc" "pCylinderShape1.iog.og[0].gco";
connectAttr "groupParts5.og" "pCylinderShape1.i";
connectAttr "groupId10.id" "pCylinderShape1.ciog.cog[0].cgid";
connectAttr "groupId1.id" "pSphereShape2.iog.og[0].gid";
connectAttr "aiStandardSurface1SG.mwc" "pSphereShape2.iog.og[0].gco";
connectAttr "groupParts1.og" "pSphereShape2.i";
connectAttr "groupId2.id" "pSphereShape2.ciog.cog[0].cgid";
connectAttr "groupParts2.og" "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.i"
		;
connectAttr "groupId3.id" "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.iog.og[0].gid"
		;
connectAttr "aiStandardSurface1SG.mwc" "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.iog.og[0].gco"
		;
connectAttr "groupId4.id" "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.ciog.cog[0].cgid"
		;
connectAttr "groupParts3.og" "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.i"
		;
connectAttr "groupId5.id" "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.iog.og[0].gid"
		;
connectAttr "aiStandardSurface1SG.mwc" "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.iog.og[0].gco"
		;
connectAttr "groupId6.id" "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.ciog.cog[0].cgid"
		;
connectAttr "groupParts8.og" "pSphere3Shape.i";
connectAttr "groupId11.id" "pSphere3Shape.iog.og[0].gid";
connectAttr "aiStandardSurface1SG.mwc" "pSphere3Shape.iog.og[0].gco";
connectAttr "groupId12.id" "pSphere3Shape.iog.og[1].gid";
connectAttr "aiStandardSurface2SG.mwc" "pSphere3Shape.iog.og[1].gco";
connectAttr "groupId13.id" "pSphere3Shape.iog.og[2].gid";
connectAttr "aiStandardSurface3SG.mwc" "pSphere3Shape.iog.og[2].gco";
connectAttr "groupParts7.og" "groupParts8.ig";
connectAttr "groupId13.id" "groupParts8.gi";
connectAttr "groupParts6.og" "groupParts7.ig";
connectAttr "groupId12.id" "groupParts7.gi";
connectAttr "polyUnite1.out" "groupParts6.ig";
connectAttr "groupId11.id" "groupParts6.gi";
connectAttr "pSphereShape2.o" "polyUnite1.ip[0]";
connectAttr "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.o" "polyUnite1.ip[1]"
		;
connectAttr "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.o" "polyUnite1.ip[2]"
		;
connectAttr "pSphereShape1.o" "polyUnite1.ip[3]";
connectAttr "pCylinderShape1.o" "polyUnite1.ip[4]";
connectAttr "pSphereShape2.wm" "polyUnite1.im[0]";
connectAttr "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.wm" "polyUnite1.im[1]"
		;
connectAttr "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.wm" "polyUnite1.im[2]"
		;
connectAttr "pSphereShape1.wm" "polyUnite1.im[3]";
connectAttr "pCylinderShape1.wm" "polyUnite1.im[4]";
connectAttr "aiStandardSurface1.out" "aiStandardSurface1SG.ss";
connectAttr "pSphereShape2.iog.og[0]" "aiStandardSurface1SG.dsm" -na;
connectAttr "pSphereShape2.ciog.cog[0]" "aiStandardSurface1SG.dsm" -na;
connectAttr "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.iog.og[0]" "aiStandardSurface1SG.dsm"
		 -na;
connectAttr "|group|pasted__pSphere2|transform4|pasted__pSphereShape2.ciog.cog[0]" "aiStandardSurface1SG.dsm"
		 -na;
connectAttr "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.iog.og[0]" "aiStandardSurface1SG.dsm"
		 -na;
connectAttr "|group1|pasted__pSphere2|transform3|pasted__pSphereShape2.ciog.cog[0]" "aiStandardSurface1SG.dsm"
		 -na;
connectAttr "pSphere3Shape.iog.og[0]" "aiStandardSurface1SG.dsm" -na;
connectAttr "groupId1.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId2.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId3.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId4.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId5.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId6.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "groupId11.msg" "aiStandardSurface1SG.gn" -na;
connectAttr "aiStandardSurface1SG.msg" "materialInfo1.sg";
connectAttr "aiStandardSurface1.msg" "materialInfo1.m";
connectAttr "aiStandardSurface1.msg" "materialInfo1.t" -na;
connectAttr "polySphere2.out" "groupParts1.ig";
connectAttr "groupId1.id" "groupParts1.gi";
connectAttr "pasted__polySphere2.out" "groupParts2.ig";
connectAttr "groupId3.id" "groupParts2.gi";
connectAttr "pasted__polySphere3.out" "groupParts3.ig";
connectAttr "groupId5.id" "groupParts3.gi";
connectAttr "aiStandardSurface2.out" "aiStandardSurface2SG.ss";
connectAttr "pSphereShape1.iog.og[0]" "aiStandardSurface2SG.dsm" -na;
connectAttr "pSphereShape1.ciog.cog[0]" "aiStandardSurface2SG.dsm" -na;
connectAttr "pSphere3Shape.iog.og[1]" "aiStandardSurface2SG.dsm" -na;
connectAttr "groupId7.msg" "aiStandardSurface2SG.gn" -na;
connectAttr "groupId8.msg" "aiStandardSurface2SG.gn" -na;
connectAttr "groupId12.msg" "aiStandardSurface2SG.gn" -na;
connectAttr "aiStandardSurface2SG.msg" "materialInfo2.sg";
connectAttr "aiStandardSurface2.msg" "materialInfo2.m";
connectAttr "aiStandardSurface2.msg" "materialInfo2.t" -na;
connectAttr "polyCloseBorder1.out" "groupParts4.ig";
connectAttr "groupId7.id" "groupParts4.gi";
connectAttr "polyConnectComponents1.out" "polyCloseBorder1.ip";
connectAttr "deleteComponent1.og" "polyConnectComponents1.ip";
connectAttr "polyTweak3.out" "deleteComponent1.ig";
connectAttr "polyExtrudeFace1.out" "polyTweak3.ip";
connectAttr "polyTweak1.out" "polyExtrudeFace1.ip";
connectAttr "pSphereShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polySphere1.out" "polyTweak1.ip";
connectAttr "aiStandardSurface3.out" "aiStandardSurface3SG.ss";
connectAttr "pCylinderShape1.iog.og[0]" "aiStandardSurface3SG.dsm" -na;
connectAttr "pCylinderShape1.ciog.cog[0]" "aiStandardSurface3SG.dsm" -na;
connectAttr "pSphere3Shape.iog.og[2]" "aiStandardSurface3SG.dsm" -na;
connectAttr "groupId9.msg" "aiStandardSurface3SG.gn" -na;
connectAttr "groupId10.msg" "aiStandardSurface3SG.gn" -na;
connectAttr "groupId13.msg" "aiStandardSurface3SG.gn" -na;
connectAttr "aiStandardSurface3SG.msg" "materialInfo3.sg";
connectAttr "aiStandardSurface3.msg" "materialInfo3.m";
connectAttr "aiStandardSurface3.msg" "materialInfo3.t" -na;
connectAttr "polyExtrudeFace2.out" "groupParts5.ig";
connectAttr "groupId9.id" "groupParts5.gi";
connectAttr "polyTweak2.out" "polyExtrudeFace2.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyCylinder1.out" "polyTweak2.ip";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "aiStandardSurface1SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "aiStandardSurface2SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "aiStandardSurface3SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "aiStandardSurface1SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "aiStandardSurface2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "aiStandardSurface3SG.message" ":defaultLightSet.message";
connectAttr "aiStandardSurface1SG.pa" ":renderPartition.st" -na;
connectAttr "aiStandardSurface2SG.pa" ":renderPartition.st" -na;
connectAttr "aiStandardSurface3SG.pa" ":renderPartition.st" -na;
connectAttr "aiStandardSurface1.msg" ":defaultShaderList1.s" -na;
connectAttr "aiStandardSurface2.msg" ":defaultShaderList1.s" -na;
connectAttr "aiStandardSurface3.msg" ":defaultShaderList1.s" -na;
// End of T5Pointer.ma