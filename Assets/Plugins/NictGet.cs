using System;
using System.Collections;
//using System.Collections.Generic;

//using System.Net.Http;
//using System.Threading.Tasks;

using UnityEngine.Networking;

using UnityEngine;

public class NictResponse
{
    string id;
    string it;
    public string st;
    string leap;
    string next;
    string step;

    public static NictResponse from_json(string data)
    {
        return JsonUtility.FromJson<NictResponse>(data);
    }
}

public class NictGet : MonoBehaviour
{
    int last_minu = -1;
    int last_hour = -1;
    int last_seco = -1;
    bool is_connecting = false;

    IEnumerator GetRoutine()
    {
        // https://qiita.com/ponchan/items/65aeb43e8fea8da0bcac
        //Debug.Log("start routine");
        UnityWebRequest web_req = UnityWebRequest.Get("http://ntp-a1.nict.go.jp/cgi-bin/json");
        yield return web_req.SendWebRequest();

        if (web_req.isNetworkError) {
            Debug.Log("[ERR]" + web_req.error);
            last_minu = -1;
        }
        else {
            string body = web_req.downloadHandler.text;
            NictResponse rsp = NictResponse.from_json(body);
            int st = (int)(float.Parse(rsp.st));

            var date = DateTimeOffset.FromUnixTimeSeconds(st).LocalDateTime;
            last_hour = date.Hour;
            last_minu = date.Minute;
            last_seco = date.Second;
            is_connecting = false;
            Debug.Log("[OK]" + st + " > " + last_hour + ":" + last_minu);
        }
    }

    public void exec()
    {
        Debug.Log("start get");
        is_connecting = true;
        IEnumerator rou = GetRoutine();
        StartCoroutine(rou);
    }

    public (int,int,int) date_get()
    {
        int ret_h = last_hour;
        int ret_m = last_minu;
        int ret_s = last_seco;
        if (!is_connecting) {
            last_hour = -1;
            last_minu = -1;
            last_seco = -1;
        }
        return (ret_h, ret_m, ret_s);
    }
}
