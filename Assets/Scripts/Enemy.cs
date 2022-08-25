using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public Node2D target;
    public Pathfinding2D ptf;
    public Transform player;
    public List<Node2D> path => ptf.path;

    private Grid2D gr;

    private void Start()
    {
        ptf = GetComponent<Pathfinding2D>();
        gr = FindObjectOfType<Grid2D>();
        player = FindObjectOfType<Player>().transform;
        ptf.FindPath(transform.position, player.position);
        target = path[1];
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 1)
        {
            if (transform.position != target.worldPosition)
            {
                if (Vector3.Distance(transform.position, target.worldPosition) < 2)
                {
                    if (path[0].obstacle)
                    {
                        gr.ObjectFromWorldPoint(path[0].worldPosition).GetComponent<Wall>().Damage(.5f);
                    }
                    transform.position = Vector3.MoveTowards(transform.position, target.worldPosition, .004f);
                }
                else
                {
                    ptf.FindPath(transform.position, player.position);
                    target = ptf.path[0];
                }
            }
            else
            {
                ptf.FindPath(transform.position, player.position);
                target = ptf.path[0];
            }
        }
    }
}