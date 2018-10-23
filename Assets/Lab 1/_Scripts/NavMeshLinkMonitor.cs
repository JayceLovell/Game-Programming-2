using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NavMeshLinkMonitor : MonoBehaviour
{

    private NavMeshAgent _agent;

    public NavMeshLinkMonitor(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public OffMeshLinkEvent AgentEnteredLink = new OffMeshLinkEvent();
    public OffMeshLinkEvent AgentExitedLink = new OffMeshLinkEvent();

    public bool IsOnLink;

    private OffMeshLinkData _data;
    private NavMeshSurface _startSurface;
    private NavMeshLink _link;

    public void Check()
    {
        if (_agent.isOnOffMeshLink && !IsOnLink)
        {
            var link = _agent.navMeshOwner as NavMeshLink;
            if (link != null)
            {
                _link = link;
            }
            _data = _agent.currentOffMeshLinkData;
            AgentEnteredLink?.Invoke(new NavMeshEventData
            {
                Agent = _agent,
                LinkData = _data,
            });
            IsOnLink = true;
        }
        else if (!_agent.isOnOffMeshLink && IsOnLink)
        {
            AgentExitedLink?.Invoke(new NavMeshEventData
            {
                Agent = _agent,
                LinkData = _data,
            });
            IsOnLink = false;
        }
    }

    public class NavMeshEventData
    {
        public NavMeshAgent Agent { get; set; }
        public OffMeshLinkData LinkData { get; set; }
    }

    [System.Serializable]
    public class OffMeshLinkEvent : UnityEvent<NavMeshEventData>
    {

    }
}