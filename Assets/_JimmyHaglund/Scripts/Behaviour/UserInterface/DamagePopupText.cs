using UnityEngine;
using TMPro;

namespace JimmyHaglund.Behaviours.UserInterface {
    public class DamagePopupText : MonoBehaviour {
        [SerializeField] private RectWorldAnchor _anchorTemplate = null;

        public void SpawnAnchor(Vector3 worldPosition, string popupText) {
            if (_anchorTemplate == null) return;
            var newAnchor = Instantiate(_anchorTemplate, _anchorTemplate.transform.parent);
            newAnchor.WorldPosition = worldPosition;
            newAnchor.gameObject.SetActive(true);

            var text = newAnchor.GetComponentInChildren<TMP_Text>();
            if (text == null) return;
            text.text = popupText;
        }
    }
}