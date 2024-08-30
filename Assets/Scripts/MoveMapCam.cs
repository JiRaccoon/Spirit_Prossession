using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMapCam : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _moveobjL;
    [SerializeField] private GameObject _moveobjR;
    private grid_tistory _gt;
    private int _tmp;

    private void Start()
    {
        _gt = GetComponent<grid_tistory>();
    }

    public void MoveTriggerinstantiate()
    {

        // 만약에 플레이어가 한 명이라도 여기로 왔다면 으로 가정
        if (_gt.tileMapNumber == 0)
        {
            _tmp = 9;
            Vector3 posx = new Vector3(_tmp, 0, 0);
            _moveobjR.gameObject.transform.position = posx;

            _moveobjR.gameObject.SetActive(true);
        }
        else
        {
            Vector3 posx1 = new Vector3(_tmp, 0, 0);
            _moveobjL.gameObject.transform.position = posx1;
            Vector3 posx2 = new Vector3((_tmp + 18),0,0);
            _moveobjR.gameObject.transform.position = posx2;

            _moveobjL.gameObject.SetActive(true);
            _moveobjR.gameObject.SetActive(true);
        }
    }

    public IEnumerator CameraMove(int type)
    {
        yield return null;

        Vector3 targetpos;

        if (type == 0)
        {
            targetpos = new Vector3(_cam.transform.position.x - 18, 0, -10);
        }
        else
        {
            targetpos = new Vector3(_cam.transform.position.x + 18, 0, -10);
        }

        float currenttime = 0f;
        float targettime = 2f;

       Vector3 currenpos = _cam.transform.position;

        // 카메라 움직이기
        while(currenttime < targettime)
        {
            currenttime += Time.deltaTime;
            float t = Mathf.Clamp01(currenttime / targettime); // 0과 1 사이로 클램프
            _cam.transform.position = Vector3.Lerp(currenpos, targetpos, t);
            if (currenttime >= targettime) break;
            yield return null;
        }
        _cam.transform.position = targetpos;

        yield return null;
        
        Debug.Log("꺼짐");

        //정리
        if (type == 0)
        {
            if(_gt.tileMapNumber == 1)
            {
                _gt.tileMapNumber--;
            }
            else
            {
                _tmp -= 18;
                _gt.tileMapNumber--;
            }
        }
        else
        {
            if (_gt.tileMapNumber == 0)
            {
                _gt.tileMapNumber++;
            }
            else
            {
                _tmp += 18;
                _gt.tileMapNumber++;
            }
        }

        
    }
}
