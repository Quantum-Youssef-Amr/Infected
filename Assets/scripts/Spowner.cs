using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Spowner : MonoBehaviour
{
    public static int killed;
    [SerializeField] private AnimationCurve spownRate;
    [SerializeField] private float Cooldown = 0.3f, HardnessChangeRate = 0.05f;
    [SerializeField] private int WaveNumber = 1, AddNewEnemyAfter = 2;
    [SerializeField] private GameObject[] enemys = new GameObject[0];
    [SerializeField] private UpgradeSystem upgradeSystem;

    private List<SpownData> spowns = new List<SpownData>();
    public float prograss { get; private set; }
    private int _spownIndex, _allowedenemy;
    private Camera _mc;
    private Coroutine _spowning;
    void Start()
    {
        _mc = Camera.main;
        Startwave();
    }

    void Update()
    {
        prograss = (float)killed / spowns.Count;
    }

    private void Startwave()
    {
        SetWaveData();
        if(_spowning == null) _spowning = StartCoroutine(Spown());
    }

    private IEnumerator Spown()
    {
        yield return new WaitUntil(() => { return !PublicData.pause && !PublicData.gameover && !PublicData.upgradeing; });
        if (_spownIndex != spowns.Count)
        {
            Instantiate(spowns[_spownIndex].obj, spowns[_spownIndex].pos, Quaternion.identity, transform);
            _spownIndex++;
        }

        if (transform.childCount == 0 && _spownIndex == spowns.Count)
        {
            Upgrades();
            PublicData.OnUpgrade?.Invoke(true);

            yield return new WaitUntil(() =>
            {
                return !PublicData.upgradeing;
            });

            resetWave();
            yield return null;
        }

        yield return new WaitForSeconds(Cooldown);
        _spowning = null;
        if (_spowning == null) _spowning = StartCoroutine(Spown());
    }

    private void resetWave()
    {
        _spownIndex = 0;
        killed = 0;
        spowns = new List<SpownData>();

        AdvanceWave();
        Startwave();
    }

    private void AdvanceWave()
    {
        WaveNumber++;
        PublicData.waveNum = WaveNumber;
        Cooldown -= HardnessChangeRate;
    }

    public void Upgrades()
    {
        PublicData.upgradeing = true;
        upgradeSystem.ShowUpgrades();
    }

    private void SetWaveData()
    {
        if (WaveNumber % AddNewEnemyAfter == 0) _allowedenemy++;
        for (int i = 0; i < getEnemyNember(); i++)
        {
            GameObject e = enemys[UnityEngine.Random.Range(0, _allowedenemy % enemys.Length)];
            spowns.Add(new SpownData(e, getRdPos()));
        }
    }

    private Vector2 getRdPos()
    {
        Vector2 pos = UnityEngine.Random.insideUnitCircle;
        pos.Normalize();
        pos *= 2.5f * _mc.orthographicSize;
        return pos;
    }

    private int getEnemyNember()
    {
        return Mathf.FloorToInt(spownRate.Evaluate(WaveNumber));
    }

}

[Serializable]
class SpownData
{
    public GameObject obj;
    public Vector2 pos;

    public SpownData(GameObject obj, Vector2 pos)
    {
        this.obj = obj;
        this.pos = pos;
    }
}
