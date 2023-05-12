var Sounds : AudioClip[];
var Lifetime:int;
var Projectile:boolean;
public var Speed = 80;
public var SpeedMax = 80;
public var SpeedMult:float = 1;

function Start () {
	Destroy(gameObject, Lifetime);

}

function Update () {
	if(!Projectile){
		
		this.GetComponent.<Rigidbody>().velocity += this.transform.forward * Time.deltaTime * Speed;
	}
	if(Speed < SpeedMax){
		Speed += SpeedMult;
	}

	
}
