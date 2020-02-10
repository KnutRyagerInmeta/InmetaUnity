using UnityEngine;
using System.Collections;

public class EjectingCase : MonoBehaviour {
	public GameObject emptyCasingPrefab;
	public Transform ejectionPoint;

	IEnumerator Start() {
		while(true) {
			GameObject emptyCasing = Instantiate(emptyCasingPrefab, ejectionPoint.position, ejectionPoint.rotation);
			// Remove Bullet
			Destroy(emptyCasing.transform.GetChild(0).gameObject);

			Rigidbody emptyCaseRigidbody = emptyCasing.GetComponent<Rigidbody>();
			emptyCaseRigidbody.velocity = ejectionPoint.TransformDirection(-Vector3.left * 2);
			emptyCaseRigidbody.AddForce(0, Random.Range(2f, 4f), 0, ForceMode.Impulse);
			emptyCaseRigidbody.AddTorque(Random.Range(-0.5f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(-0.5f, 0.5f));

			Destroy(emptyCasing, 5f);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
