using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider hitCol;
        public Collider bottomCol;
        public Rigidbody rigid;

        public string id;
        public int hp { get; private set; }
        public int weaponId;

        public int maxHp { get; private set; } = 100;
        public bool isLive { get; private set; } = true;

        [HideInInspector] public CharacterInput inputController;
        [HideInInspector] public CharacterAnimation animController;
        [HideInInspector] public CharacterAttack attackController;
        [HideInInspector] public FollowCam followCam;

        [SerializeField] private Canvas canvasInfo;
        [SerializeField] private Slider sliderHP;
        [SerializeField] private Text textNickname;

        private void LateUpdate()
        {
            if (id != GameManager.instance.currentUser.ID)
                return;

            SendPacket();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (inputController != null && other.gameObject.tag == StringData.TagMap)
            {
                inputController.InGround();
            }
        }
        public void CanAttack()
        {
            if(inputController != null)
                inputController.CanAttack();
        }
        public void HitWeapon(Weapon _weapon)
        {
            if (inputController != null && _weapon.playerID != id)
            {
                _weapon.gameObject.SetActive(false);
                hp -= _weapon.atkDamage;
                if (hp <= 0)
                {
                    Death();
                }
            }
        }
        public void SetInfo(UserData _data)
        {
            id = _data.ID;
            hp = maxHp;
            textNickname.text = _data.Nickname;

            if (GameManager.instance.currentUser.ID == id)
            {
                inputController = gameObject.AddComponent<CharacterInput>();
                canvasInfo.gameObject.SetActive(false);
            }
            animController = gameObject.AddComponent<CharacterAnimation>();
            attackController = gameObject.AddComponent<CharacterAttack>();
        }
        public void Death()
        {
            isLive = false;
        }
        public void ClearData()
        {
            Destroy(inputController);
            Destroy(hitCol);
            Destroy(rigid);
        }
        private void SendPacket()
        {
            CharacterPacket packet = new CharacterPacket()
            {
                id = id,
                hp = hp,
                position = transform.localPosition,
                rotation = transform.localRotation,
                animation = animController.animationType
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Character,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void DoSync(int _hp, Vector3 _position, Quaternion _rotation, AnimationType _anim)
        {
            hp = _hp;
            transform.localPosition = _position;
            transform.localRotation = _rotation;
            animController.DoAnimation(_anim);
            sliderHP.value = (float)hp / (float)maxHp;
        }
    }
}