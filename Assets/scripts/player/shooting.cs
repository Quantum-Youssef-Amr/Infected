using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    [SerializeField] public float Cooldown;
    [SerializeField] public GameObject Gun, Bult;
    [SerializeField] public AudioSource shoot;
    [SerializeField] public int bolts = 1000;
    [SerializeField] public int boltsInRound = 30;
    [SerializeField] public float reloading_Time = 0.5f;

    private Transform _t;
    private Camera _main;
    public int _bolts_shoot { get; set; }
    private Coroutine co;
    public bool _canshoot = false, _reloading = false;

    public int Rate { get { return (int)(1f / Cooldown); } }

    #region inputs
    private Input_system inputs;

    private void Awake()
    {
        inputs = new Input_system();
        _t = transform;
        _main = Camera.main;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void Osable()
    {
        inputs.Disable();
    }

    #endregion

    void Start()
    {
        inputs.Player.Fire.performed += _ => { Fire(); };
        inputs.Player.Reloud.performed += _ => { if (co == null) StartCoroutine(reload()); };
    }

    void Update()
    {
        if (inputs.Player.Fire.IsInProgress() && PublicData.setting.holdtofire)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        if (PublicData.pause || PublicData.gameover) return;
        if (co == null && _bolts_shoot >= boltsInRound) co = StartCoroutine(reload());
    }

    public void Fire()
    {
        if (PublicData.pause || PublicData.gameover || _bolts_shoot >= boltsInRound) return;
        if (co == null) co = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (_canshoot && !PublicData.upgradeing)
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

    private IEnumerator reload()
    {
        if (_bolts_shoot != 0)
        {
            _reloading = true;
            _canshoot = false;

            bolts -= boltsInRound;
            _bolts_shoot = 0;
        }
        yield return null;
        co = null;
    }

    public GameObject getBult()
    {
        return Bult;
    }

}
