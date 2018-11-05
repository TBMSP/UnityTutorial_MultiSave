using net.TBMSP.Lib.File;
using net.TBMSP.Lib.TBMPk.File;
using UnityEngine;
namespace tutorial.MultiSave{
public class GameManager:MonoBehaviour{
public Transform player,LoadMenu,SaveMenu;
public int Health=100;
private static string[] Slots=new string[4];
private static Transform p;
public static GameManager GetComponent(){return p.GetComponent<GameManager>();}
void Awake(){
p=this.transform;
net.TBMSP.Lib.Program.Init(Application.persistentDataPath+"/",true);
Debug.Log("En esta carpeta se guardan los archivos: \""+net.TBMSP.Lib.Program.RootDirectory+"\"");
FileBase.CreateDirectory("Data/SaveData");
for(int i=0;i<Slots.Length;i++){
Slots[i]=TDF.CreateString();
}
}
void Start(){
player.GetComponent<Player>().Move=false;
SaveMenu.gameObject.SetActive(false);
LoadMenu.gameObject.SetActive(true);
}
void Update(){
if(Input.GetKeyDown(KeyCode.Z)){
player.GetComponent<Player>().Move=false;
SaveMenu.gameObject.SetActive(false);
LoadMenu.gameObject.SetActive(true);
}
if(Input.GetKeyDown(KeyCode.X)){
OpenSaveMenu();
}
}
public void saveGame(int SlotID){
SaveGame(SlotID);
SaveMenu.gameObject.SetActive(false);
player.GetComponent<Player>().Move=true;
}
public void loadGame(int SlotID){
if(FileBase.FileExist("Data/SaveData/Slot_"+(SlotID+1)+".tdf")){
Slots[SlotID]=TDF.Load("Data/SaveData/Slot_"+(SlotID+1)+".tdf");//TDF.LoadGZ
var playerComp=GetComponent().player.GetComponent<Player>();
var pos=TDF.GetValueOfBlock(Slots[SlotID],"Game", "PlayerPos").Split(","[0]);
var posX=float.Parse(pos[0]);
var posY=float.Parse(pos[1]);
var posZ=float.Parse(pos[2]);
player.position=new Vector3(posX,posY,posZ);
var rot=TDF.GetValueOfBlock(Slots[SlotID],"Game", "PlayerRot").Split(","[0]);
var rotX=float.Parse(rot[0]);
var rotY=float.Parse(rot[1]);
var rotZ=float.Parse(rot[2]);
playerComp.rotation=new Vector3(rotX,rotY,rotZ);
player.rotation=Quaternion.Euler(new Vector3(0,rotY,0));
Health=int.Parse(TDF.GetValueOfBlock(Slots[SlotID],"Game","PlayerHealth"));
}
player.GetComponent<Player>().Move=true;
SaveMenu.gameObject.SetActive(false);
LoadMenu.gameObject.SetActive(false);
}
public static void OpenSaveMenu(){
GetComponent().SaveMenu.gameObject.SetActive(true);
GetComponent().LoadMenu.gameObject.SetActive(false);
GetComponent().player.GetComponent<Player>().Move=false;
}
public static void SaveGame(int SlotID){
var playerComp=GetComponent().player.GetComponent<Player>();
Slots[SlotID]=TDF.SaveValueInBlock(Slots[SlotID],"Game","PlayerPos",GetComponent().player.position.x+","+GetComponent().player.position.y+","+GetComponent().player.position.z);
Slots[SlotID]=TDF.SaveValueInBlock(Slots[SlotID],"Game","PlayerRot",playerComp.rotation.x+","+playerComp.rotation.y+","+playerComp.rotation.z);
Slots[SlotID]=TDF.SaveValueInBlock(Slots[SlotID],"Game","PlayerHealth",GetComponent().Health.ToString());
TDF.Save("Data/SaveData/Slot_"+(SlotID+1)+".tdf",Slots[SlotID]);//TDF.SaveGZ
}
}
}