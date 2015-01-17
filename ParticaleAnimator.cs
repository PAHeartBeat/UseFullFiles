using UnityEngine;

public class ParticaleAnimator : MonoBehaviour {

	private ParticleSystem pSystem;
	private double lastInterval;
	private float deltaTime;
	private bool isSimulate = false;

	public void Awake() {
		lastInterval = Time.realtimeSinceStartup;
		pSystem = gameObject.GetComponent<ParticleSystem>();
		isSimulate = pSystem.playOnAwake;
	}

	public void Update() {
		if(!isSimulate) {
			return;
		}
		deltaTime += Time.realtimeSinceStartup - (float)lastInterval;
		lastInterval = Time.realtimeSinceStartup;
		if(Time.timeScale == 0) {
			if(deltaTime >= pSystem.duration && pSystem.loop) {
				deltaTime -= pSystem.duration;
			} else if(deltaTime >= pSystem.duration && !pSystem.loop) {
				Stop();
			}
			pSystem.Simulate(deltaTime);
			pSystem.Play();
		}
	}

	public void Simulate() {
		Debug.Log("Start Particle: " + name);
		deltaTime = 0;
		lastInterval = Time.realtimeSinceStartup;
		isSimulate = true;
	}
	public void Stop() {
		isSimulate = false;
		deltaTime = 0;
		pSystem.Stop();
	}
}