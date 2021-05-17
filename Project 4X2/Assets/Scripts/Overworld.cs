using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class Overworld : Clickable
    {
        public AIDestinationSetter Destination { get => GetComponentInParent<AIDestinationSetter>(); }

        public AudioClip SelectionAudio;
        public AudioClip AttackLine;
        public AudioClip MoveingAudio;

        public LayerMask EnemyLayers;
        public int AttackStrength, Health;
        public float AttackRange, MoveSpeed;
        public bool Moving, EnemyInRange, AttackMove;

        public Animator[] Animators;

        private void Awake()
        {
            Animators = GetComponentsInChildren<Animator>();
            GetComponentInParent<AIPath>().maxSpeed = MoveSpeed;
        }

        private void Update()
        {
            Moving = UnitMovement();

            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, AttackRange, EnemyLayers);

            if (Destination.target != null)
            {
                if (GetComponentInParent<AIPath>().reachedEndOfPath && Destination.target.GetComponent<SpriteRenderer>() == true)
                {
                    Destination.target.gameObject.SetActive(false);
                }
            }
        }

        public bool UnitMovement()
        {
            if (Destination.target != null)
            {
                bool destinationreached = !GetComponentInParent<AIPath>().reachedEndOfPath;
                if (!destinationreached)
                {
                    foreach (Animator ani in Animators)
                    {
                        ani.SetTrigger("Idle");
                    }
                }
                return destinationreached;
            }
            return false;
        }

        public void MakeAttackMove(Clickable Enemy)
        {
            OverWorldUnitManager.Instance.Enemy = Enemy;
            OverWorldUnitManager.Instance.UnitVoiceLines.clip = AttackLine;
            OverWorldUnitManager.Instance.UnitVoiceLines.Play();
            AttackMove = true;
        }

        protected override void Clicked()
        {
            base.Clicked();

            if (transform.parent.transform.tag == "Player Unit")
            {
                if (OverWorldUnitManager.Instance.CurrentSelection != this)
                {
                    OverWorldUnitManager.Instance.CurrentSelection = this;
                    OverWorldUnitManager.Instance.UnitVoiceLines.clip = SelectionAudio;
                    OverWorldUnitManager.Instance.UnitVoiceLines.Play();
                }
                Debug.Log("SIR!");
            }

        }

    }
}