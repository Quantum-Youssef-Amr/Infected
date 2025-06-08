using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class Spowner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private GameObject[] FObjects;
    [SerializeField] private float Rate, ChangeRate, SpawnRad = 1,waveNum = 1, EparWave = 10;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private UpgradeSystem upgrade;

    private Camera _main;
    private float _cameraSize;
    private int _eSpowned;
    public static int _tospawn;

    private void Awake()
    {
        _main = Camera.main;
        _cameraSize = _main.orthographicSize;
        PublicData.upgradeing = false;

        _tospawn = Mathf.FloorToInt(waveNum * EparWave);
    }

    float _timer;
    Vector2 m_spownLoc;
    Coroutine su;
    private void Update()
    {
        if (PublicData.pause || PublicData.gameover) return;

        waveText.text = "wave " + waveNum;

        if (_timer < (1 / Rate) * waveNum) _timer += Time.deltaTime;
        else
        {
            _timer = 0;
            if (_eSpowned < _tospawn)
            {
                spown();
                _eSpowned++;
            }
            else
            {
                if (shooting.Killed == _tospawn)
                {
                    if (PublicData.upgradeing == false && su == null)
                        su = StartCoroutine(startUpgrade());
                }
            }
        }
    }

    private int index;
    public void nextWave()
    {
        Random.InitState(Random.Range(0,1000000));

        if (index != FObjects.Length)
        {
            if (waveNum % 2 == 0)
            {
                objects.Add(FObjects[index]);
                index++;
            }
        }

        waveNum++;
        _eSpowned = 0;
        _tospawn = Mathf.FloorToInt(waveNum * EparWave);
        shooting.Killed = 0;
        Rate += ChangeRate;

        PublicData.waveNum = (int) waveNum;
    }

    private void spown()
    {
        _cameraSize = _main.orthographicSize;

        m_spownLoc = Random.onUnitSphere;
        m_spownLoc.Normalize();
        m_spownLoc *= _cameraSize * (2 + ((float)Screen.height / Screen.width)) * SpawnRad;
        m_spownLoc = new Vector2(m_spownLoc.x + Mathf.Sign(m_spownLoc.x)* (1980f/Screen.currentResolution.width), m_spownLoc.y + Mathf.Sign(m_spownLoc.y)* (1080f/Screen.currentResolution.height));
        
        Instantiate(objects[Random.Range(0, objects.Count)], m_spownLoc, Quaternion.identity);
    }

    IEnumerator startUpgrade()
    {
        upgrade.ShowUpgrades();
        PublicData.upgradeing = true;
        yield return new WaitUntil(() => { return !PublicData.upgradeing; });
        nextWave();
        su = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SpawnRad * Camera.main.orthographicSize * (2 + ((float)Screen.height / Screen.width)));
    }
}

