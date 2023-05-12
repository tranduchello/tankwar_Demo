#pragma strict
var Guns:GameObject[];
var CurrentGun:int;
var Shooting:boolean;
var CoolDown:int = 0;
//var myfxManager:GameObject[];  					//my addon
//var DoGunFX:GameObject[]; 		//my addon





function Start () {
	CurrentGun = 0;
	GameObject.Find("infotext").GetComponent.<GUIText>().text = "Heat Seeker Missile";
	
	 

	
	
	
}

function Update () {
	if(Input.GetKeyDown(KeyCode.Alpha1)){
		CurrentGun = 0;
		GameObject.Find("infotext").GetComponent.<GUIText>().text = "Heat Seeker Missile";
	}
	if(Input.GetKeyDown(KeyCode.Alpha2)){
		CurrentGun = 1;
		GameObject.Find("infotext").GetComponent.<GUIText>().text = "Minigun";
	}
	if(Input.GetKeyDown(KeyCode.Alpha3)){
		CurrentGun = 2;
		GameObject.Find("infotext").GetComponent.<GUIText>().text = "Storm Rocket";
	}
	if(Input.GetKeyDown(KeyCode.Alpha4)){
		CurrentGun = 3;
		GameObject.Find("infotext").GetComponent.<GUIText>().text = "Cannon";
	}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
		CurrentGun = 4;
		GameObject.Find("infotext").GetComponent.<GUIText>().text = "Naparm Missile";
	}
	if(Input.GetMouseButtonUp(0)){
		Shooting = false;
	}
	if(Input.GetMouseButtonDown(0)){
		Shooting = true;
	}

	
	if(Shooting){
		Guns[CurrentGun].gameObject.GetComponent(WeaponLauncher).Shoot(); // Just Call Shoot(); the gun will fire.
		
	}
	
	 				

	
}