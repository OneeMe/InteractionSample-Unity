using UnityEngine;

namespace PXR.Benchmark.UI
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Camera _targetCamera;
        // Update is called once per frame

        private void Start()
        {
            if (_targetCamera == null)
            {
                _targetCamera = Camera.main;
            }
        }
        void Update()
        {
            transform.LookAt(_targetCamera.transform.position, Vector3.up);
        }
    }
}
