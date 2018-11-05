using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tutorial.MultiSave{
public class SaveTrigger:MonoBehaviour{
public bool InSave=false;
void OnTriggerEnter(Collider c){
if(c.tag=="SaveItem"&&!InSave){
GameManager.OpenSaveMenu();
InSave=true;
}
}
void OnTriggerExit(Collider c){
if(c.tag=="SaveItem"&&InSave){
InSave=false;
}
}
}
}