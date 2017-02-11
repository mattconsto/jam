﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ConversationLoader : MonoBehaviour {

	ArrayList conversations = new ArrayList ();
	ArrayList drives = new ArrayList ();
	ArrayList requests = new ArrayList ();
	void Awake()
	{
		loadFromFile(conversations,"convo.txt");
		loadFromFile(drives,"drive.txt");
		loadFromFile(requests,"request.txt");
	}
	
	void loadFromFile(ArrayList list, string filename)
	{
		StringBuilder convstr;
		StreamReader rdr = new StreamReader (Application.dataPath + "/ConversationFiles/" + filename, Encoding.Default);
		using(rdr)
		{
			string line;
			do
			{
				line = rdr.ReadLine();
				if(line!=null)
				{
					if(line.Equals("***"))
						list.Add (new Conversation());
					else
					{
						((Conversation)list[list.Count-1]).addSentance(line);
						conversations.Add (new Conversation());
					}

				}
			}
			while(line != null);
			rdr.Close();
		}
	}
	public Conversation choseRandomConversation()
	{
		int convo = Random.Range (0, (conversations.Count - 1));
		((Conversation)conversations [convo]).reset ();
		return (Conversation)conversations [convo];
	}
	public Conversation getDriveConversation(int day)
	{
		return (Conversation)drives[day];
	}
	public Conversation getRequest()
	{
		return (Conversation)requests [0];
	}
	void accessData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				Debug.Log(key);
				accessData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			foreach(JSONObject j in obj.list){
				accessData(j);
			}
			break;
		case JSONObject.Type.STRING:
			Debug.Log(obj.str);
			break;
		case JSONObject.Type.NUMBER:
			Debug.Log(obj.n);
			break;
		case JSONObject.Type.BOOL:
			Debug.Log(obj.b);
			break;
		case JSONObject.Type.NULL:
			Debug.Log("NULL");
			break;
			
		}
	}
}