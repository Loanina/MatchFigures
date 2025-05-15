using UnityEngine;

namespace Figure.Types.Sticky
{
    public class StickyTrigger : MonoBehaviour
    {
        private Rigidbody2D ownRb;
        private bool hasStuck = false;

        public void Init(Rigidbody2D rb)
        {
            ownRb = rb;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasStuck) return;

            var otherRb = collision.rigidbody;
            if (otherRb == null || otherRb == ownRb) return;

            var joint = otherRb.gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = ownRb;
            joint.autoConfigureConnectedAnchor = true;

            hasStuck = true;
            Destroy(this);
        }
    }
}