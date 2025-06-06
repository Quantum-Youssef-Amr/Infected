using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shooting : MonoBehaviour
{
    [SerializeField] public float Cooldown;
    [SerializeField] public GameObject Gun, Bult;
    [SerializeField] public AudioSource shoot;
    [SerializeField] public Slider prograss;

    [SerializeField] public int bolts = 1000;
    [SerializeField] public int boltsInRound = 30;
    [SerializeField] public float reloading_Time = 0.5f;

    private Transform _t;
    private Camera _main;
    public int _bolts_shoot {get; set;}
    private Coroutine co;
    public bool _canshoot = false, _reloading = false;
    public static int Killed;

    public int Rate { get { return (int)(1f / Cooldown); }}

    private void Awake()
    {
        _t = transform;
        _main = Camera.main;
    }

    private void FixedUpdate()
    {
        if (PublicData.pause || PublicData.gameover) return;
        float pro = (float) Killed / Spowner._tospawn;
        prograss.value = pro;
            
        if(co==null && _bolts_shoot >= boltsInRound) co = StartCoroutine(reload());
        if(Application.isMobilePlatform){
            if(controler.conMag > 1f  && _bolts_shoot < boltsInRound){
                if(co == null) co = StartCoroutine(Shoot());
            }
        }else{
            if (Input.GetKey(KeyCode.Mouse0) && _bolts_shoot < boltsInRound)
            {    
                if(co == null) co = StartCoroutine(Shoot());
            }
        }
    }

    public IEnumerator Shoot()
    {
        if(_canshoot && !PublicData.upgradeing)
        {
            _canshoot = false;
            _bolts_shoot++;
            Instantiate(Bult, Gun.transform.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, _t.up)));
            shoot.Play();
        }
        yield return new WaitForSeconds(Cooldown);
        _canshoot = true;
        co = null;
    }

    public IEnumerator reload(){
        _reloading = true;
        _canshoot = false;

        yield return new WaitForSeconds(reloading_Time);

        bolts -= boltsInRound;
        _bolts_shoot=0;

        _canshoot = true;
        _reloading = false;
        co = null;
    }

}
