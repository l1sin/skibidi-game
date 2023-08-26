using UnityEngine;
using UnityEngine.Playables;

public class ZaWarudo : MonoBehaviour
{

    [SerializeField] private GameObject _postproc;
    [SerializeField] private bool _timeIsStopped;
    [SerializeField] private PlayableDirector _tStop;
    [SerializeField] private PlayableDirector _tResume;

    private void Start()
    {
        _timeIsStopped = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleTimeStop();
        }
    }

    private void ToggleTimeStop()
    {
        if (!(_tStop.state == PlayState.Playing) && !(_tResume.state == PlayState.Playing))
        {
            if (!_timeIsStopped)
            {
                _tResume.Stop();
                _tStop.Play();
                _timeIsStopped = true;

            }
            else
            {
                _tStop.Stop();
                _tResume.Play();
                _timeIsStopped = false;
            }
        }   
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    public void TimeResume()
    {
        Time.timeScale = 1;
    }

}
