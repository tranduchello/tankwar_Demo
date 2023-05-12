public var missile :GameObject;
public var noise:int;
public var Owner:GameObject;
public var Flash:GameObject;
public var FlashSize:int = 4;
public var fireRate = 0.1f;
private var nextFireTime = 0.0;
private var timeFlash :int = 0;
public var SoundGun:AudioClip[];
public var ForceShoot :int = 0;
public var NumBullet :int = 0;
public var targetlockontexture:Texture2D;
public var targetlockedtexture:Texture2D;
private var target:GameObject;
private var locked:boolean;
public var distanceLock:float = 200;
public var timetolock:float = 2;
private var timetolockcount:float = 0;
public var targettag:String = "Enemy";
public var aimdirection:float = 0.8f; 
public var Seeker:boolean;


function Start () {
	
	nextFireTime = 0.0;
	timetolockcount = Time.time;
}

function Update () {


	if(Flash){
		if(timeFlash>0){
			timeFlash-=1;
		}else{
			Flash.GetComponent.<Renderer>().enabled = false;
			if(Flash.GetComponent.<Light>())
			Flash.GetComponent.<Light>().enabled = false;
		}
	}
	
	if(Seeker){
	if(GameObject.FindGameObjectsWithTag(targettag).Length>0){
		var objs:GameObject[] = GameObject.FindGameObjectsWithTag(targettag);
		var distance = int.MaxValue;
		for(var i=0;i<objs.length;i++){
		if(objs[i]){
			var dir	= (objs[i].transform.position - transform.position).normalized;
			var direction = Vector3.Dot(dir,transform.forward);
			if(direction >= aimdirection)
			{
				if(timetolockcount + timetolock < Time.time ){
					if(!locked){
						var dis = Vector3.Distance(objs[i].transform.position, transform.position);
						if(distanceLock > dis){
							if(distance>dis){
								distance = dis;
								target = objs[i];
							}
						}
					}
				}
			}else{
				
			}
   		}
   	 }
   	 
   	 if(target){
		locked = true;
		var targetdistance = Vector3.Distance(transform.position, target.transform.position);
		if(targetdistance > distanceLock){
			Unlock();
		}
	}

	
   }
   }

}
function DrawTargetLockon(locktexture:Texture2D,aimtarget:Transform,locked:boolean){
	if(locktexture && GameObject.Find("Main Camera")){
		var screenPos : Vector3 = GameObject.Find("Main Camera").gameObject.GetComponent.<Camera>().WorldToScreenPoint(aimtarget.transform.position);
		var distance = Vector3.Distance(transform.position, aimtarget.transform.position);
		GUI.DrawTexture(new Rect(screenPos.x-32,Screen.height - screenPos.y-32,64,64),locktexture);
		GUI.Label(new Rect(screenPos.x+40, Screen.height - screenPos.y,200,30),"Target "+ Mathf.Floor(distance)+"m.");
	}else{
		Debug.Log("Can't Find (Main Camera), you can fix here by changing the (Main Camera) to any camera name you have.");
	}
}
function OnGUI(){
		if(Seeker){
		if(locked){
			if(target){
				DrawTargetLockon(targetlockedtexture,target.transform,true);
			}
		}
		if(GameObject.FindGameObjectsWithTag(targettag).Length>0){
			var objs:GameObject[] = GameObject.FindGameObjectsWithTag(targettag);
			for(var i=0;i<objs.length;i++){
				if(objs[i]){
					var dir	= (objs[i].transform.position - transform.position).normalized;
					var direction	= Vector3.Dot(dir,transform.forward);
					if(direction >= aimdirection)
					{
						var dis = Vector3.Distance(objs[i].transform.position, transform.position);
						if(distanceLock > dis){
							DrawTargetLockon(targetlockontexture,objs[i].transform,true);
						}
					}
				}
			}
		}
		}
	
}

function Unlock(){
	// Disable Target
	locked = false;
	timetolockcount = Time.time;
    target = null;
}



function Shoot(){
	
    if (Time.time > nextFireTime + fireRate){
        
    	nextFireTime = Time.time;
		if(SoundGun.length>0){
			AudioSource.PlayClipAtPoint(SoundGun[Random.Range(0,SoundGun.length)],transform.position);
		}
	
		for(var i=0;i<NumBullet;i++){
	
			var noisGun = Vector3(Random.Range(-noise,noise),Random.Range(-noise,noise),Random.Range(-noise,noise))/200;
   			var direction = transform.TransformDirection(Vector3.forward+noisGun);
    		var rotate:Quaternion = this.transform.rotation;
    
			var bullet:GameObject = Instantiate(missile, this.transform.position, rotate);
			if(bullet.GetComponent(MoverMissile)){
				bullet.GetComponent(MoverMissile).target = target;
			}
			if(bullet.GetComponent(Damage)){
				bullet.GetComponent(Damage).Owner = Owner;
			}
			if(bullet.GetComponent.<Rigidbody>()){
    			bullet.GetComponent.<Rigidbody>().AddForce(direction*ForceShoot);
    		}
    	}
    	Unlock();
    
    
    

    	timeFlash = 1;
    	if(Flash){
    		Flash.GetComponent.<Renderer>().enabled = true;
    		if(Flash.GetComponent.<Light>())
    		Flash.GetComponent.<Light>().enabled = true;
			Flash.transform.localScale.x = Random.Range(FlashSize/2,FlashSize)/100.0f;
			Flash.transform.localScale.y = Flash.transform.localScale.x;
			Flash.transform.localScale.z = Flash.transform.localScale.x;
		}
		nextFireTime += fireRate;
    }
    
}
