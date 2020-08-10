//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Minutes : MonoBehaviour
{
    const float interval = 60;
    float ellapsed = 0;
    int last_step = 0;
    int last_hour = 0;
    int adjust_time = 0;
    int adjust_interval = 15;
    bool is_ajusting = false;

    HandLong  hand_long;
    HandShort hand_short;
    NictGet nict_get;

    AudioSource[] audio_source;

    bool is_debug_mode = false;
    bool just_after_press = false;

    int step_get()
    {
        return last_step;
    }

    int hour_get()
    {
        return last_hour;
    }

    void time_signal(int hour)
    {
        if (hour%2 == 0) {
            // 1時を基準にした奇数時間に鳴らすので
            return;
        }
        int zodiac = hour / 2;
        if (DateTime.Now.Hour >= 12) {
            zodiac += 6;
        }
        audio_source[zodiac].Play();

        Debug.Log("time_signal " + hour);
    }

    void Start()
    {
        var hl_obj = GameObject.Find("hand_long");
        hand_long = hl_obj.GetComponent<HandLong>();

        var hs_obj = GameObject.Find("hand_short");
        hand_short = hs_obj.GetComponent<HandShort>();

        // 最初は適当かもしれない時間を入れておく
        last_step = DateTime.Now.Minute;
        last_hour = DateTime.Now.Hour;
        hand_long.minutes_set(step_get());
        hand_short.minutes_set(step_get(), hour_get());
        ellapsed = DateTime.Now.Second;

        nict_get = GetComponent<NictGet>();
        nict_get.exec();
        is_ajusting = true;

        var rnd_gen = new System.Random();
        adjust_time = rnd_gen.Next(0, adjust_interval);

        var audio = GameObject.Find("tsuina_voice");
        audio_source = audio.GetComponents<AudioSource>();

        var win_ctrl = GetComponent<WindowController>();
        win_ctrl.exec();
    }

    void FixedUpdate()
    {
        ellapsed += Time.deltaTime;

        if (is_debug_mode) {
            return;
        }

        if (ellapsed < interval  &&  !is_ajusting) {
            //Debug.Log("- " + ellapsed + " / " + last_step);
            //Debug.Log("> " + DateTime.Now.Second);
            return;
        }
        // else -> 調整中はこまめにポーリングする

        //int curr_sec;
        //curr_sec = DateTime.Now.Second;
        (int hour, int min, int sec) = nict_get.date_get();
        if (min != -1) {
            last_step = min;
            last_hour = hour;
            //curr_sec = sec;
            ellapsed = sec;

            var rnd_gen = new System.Random();
            adjust_time = rnd_gen.Next(0, adjust_interval);
        } else {
            if (is_ajusting) {
                return;
            }
            last_step += 1;
            //curr_sec = DateTime.Now.Second;
            ellapsed = 0;
            if (step_get() % adjust_interval == adjust_time) {
                nict_get.exec();
                is_ajusting = true; // ここではfalseのはず
                return;
            }
            if (step_get() >= 60) {
                last_step = 0;
                last_hour = (last_hour + 1) % 12;
                time_signal(hour_get());
            }
        }
        is_ajusting = false;

        hand_long.minutes_set(step_get());
        hand_short.minutes_set(step_get(), hour_get());
        //ellapsed = curr_sec;
    }

    public void on_click()
    {
        if (just_after_press) {
            just_after_press = false;
            return;
        }
        //Debug.Log("minutes::on_click " + data.button.ToString());
        if (is_debug_mode) {
            last_step += 1;
            if (last_step == 60) {
                last_hour += 1;
                last_step = 0;
            }
            if (last_step == 0) {
                time_signal(hour_get());
            }
        } else {
            nict_get.exec();
            is_ajusting = true;
        }
    }

    // -------------------------
    // デバッグコード
    bool  is_pressing = false;
    float press_start = 0;

    public void on_press()
    {
        //Debug.Log("on_press");

        is_pressing = true;
        press_start = ellapsed;
    }

    public void on_release()
    {
        //Debug.Log("on_release");

        is_pressing = false;
    }

    void Update()
    {
        if (!is_pressing) {
            return;
        }
        //Debug.Log("pressing " + ellapsed);
        if (ellapsed - press_start > 3) {
            audio_source[12].Play();
            is_debug_mode = true;
            just_after_press = true;
            return;
        }
        if (is_debug_mode  &&  ellapsed - press_start > 1) {
            last_hour -= 1;
            if (last_hour < 0) {
                last_hour = 11;
            }
            is_pressing = false;
            return;
        }
    }
}
