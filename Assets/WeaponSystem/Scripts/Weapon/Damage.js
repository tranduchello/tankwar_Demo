var effect:GameObject;
var Owner:GameObject;
var Damage:int = 20;
var Explosive:boolean;
var ExplosionRadius:float = 20;
var ExplosionForce:float = 1000;
function Start () {
	if(Owner){
		if(Owner.GetComponent.<Collider>()){
			Physics.IgnoreCollision(this.GetComponent.<Collider>(),Owner.GetComponent.<Collider>());
		}
	}
}
function Update(){
	
}
function Active(){
	if(effect){
   		var obj:GameObject = Instantiate(effect,this.transform.position,this.transform.rotation);
   		GameObject.Destroy(obj,3);
   	}
   	
   	if(Explosive)
   	ExplosionDamage();
   	
   	
   	Destroy(this.gameObject);
}

function ExplosionDamage(){
	 var hitColliders = Physics.OverlapSphere(this.transform.position, ExplosionRadius);
     for (var i = 0; i < hitColliders.Length; i++) {
           var hit = hitColliders[i];
	       if (!hit)
	            continue;

	       if(hit.gameObject.tag!="Particle" && hit.gameObject.tag!="Bullet")
		   {
		   	 if(hit.gameObject.GetComponent(DamageManager)){
				hit.gameObject.GetComponent(DamageManager).ApplyDamage(Damage);
		   	 }
		   }
		   if(hit.GetComponent.<Rigidbody>())
		   hit.GetComponent.<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0);
	 }
	 
}


function NormalDamage(collision : Collision){
	if(collision.gameObject.GetComponent(DamageManager)){
		collision.gameObject.GetComponent(DamageManager).ApplyDamage(Damage);
	}	
}

function OnCollisionEnter(collision : Collision) {
	if(collision.gameObject.tag!="Particle" && collision.gameObject.tag!="Bullet"){
		if(!Explosive)
		NormalDamage(collision);
		Active();
	}
   	
}