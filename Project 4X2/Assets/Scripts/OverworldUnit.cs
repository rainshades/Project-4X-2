using UnityEngine;
using Pathfinding;

namespace Project4X2
{
    public class OverworldUnit : Clickable
    {
        public AIDestinationSetter Destination { get => GetComponentInParent<AIDestinationSetter>(); }

        public AudioClip SelectionAudio;
        public AudioClip AttackLine;
        public AudioClip MoveingAudio;

        public LayerMask EnemyLayers;
        public int AttackStrength;
        public float AttackRange, MoveSpeed;
        public bool Moving, EnemyInRange, AttackMove, CombineMove, Sieging;

        public Animator Ani;
        public OverWorldMovement Movement; 

        private void Awake()
        {
            Ani = GetComponentInChildren<Animator>();
            GetComponentInParent<AIPath>().maxSpeed = MoveSpeed;
            Movement = GetComponent<OverWorldMovement>();
        }

        private void Update()
        {
            if (Moving)
            {
                Movement.MoveCounter(GetComponentInParent<AIPath>().endReachedDistance);

                Moving = !GetComponentInParent<AIPath>().reachedDestination && Movement.movement_range > 0;
            }
            else
            {
                GetComponentInParent<AIPath>().destination = transform.parent.position;
                Ani.SetTrigger("Idle");
            }            
        }

        public void MakeCombineMove(Clickable friendly)
        {
            CombineMove = true; 
        }

        public void MakeAttackMove(Clickable Enemy)
        {
            OverWorldSelectManager.Instance.Enemy = Enemy;
            OverWorldSelectManager.Instance.UnitVoiceLines.clip = AttackLine;
            OverWorldSelectManager.Instance.UnitVoiceLines.Play();
            AttackMove = true;
        }

        public override void Clicked()
        {
            base.Clicked();

            if (transform.parent.transform.tag == "Player Unit")
            {
                OverWorldUIController.Instance.ArmyClicked(this);
                OverWorldSelectManager.Instance.CurrentSelection = this;
                OverWorldUIController.Instance.GetComponentInChildren<ArmyUI>().OpenArmyUI(GetComponentInParent<AttatchedArmy>().Army);
                Movement.TurnOnNotifier();

                if (OverWorldSelectManager.Instance.CurrentSelection != this)
                {
                    OverWorldSelectManager.Instance.UnitVoiceLines.clip = SelectionAudio;
                    OverWorldSelectManager.Instance.UnitVoiceLines.Play();
                }
                Debug.Log("SIR!");
            }

        }

    }
}