using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret turr;
    private List<Transform> enemies = new List<Transform>();
    private float range = 3;
    public Transform tower;

    private void Start()
    {
        turr = transform.parent.GetComponent<Turret>();
    }

    private void Update()
    {
        var smallestDist = 10f;
        Transform closestEnemy = null;
        if (enemies.Count > 0)
        {
            for(int index = 0; index < enemies.Count; index++)
            {
                var enemy = enemies[index];
                var position = transform.position;
                var dist = Vector2.Distance(position, enemy.position);
                
                RaycastHit2D hit = Physics2D.Raycast(position, enemy.position - position);
                
                if(hit.transform != enemy)
                    continue;

                if (dist > (range + .25f))
                {
                    enemies.Remove(enemy);
                }
                else if (dist < smallestDist)
                {
                    smallestDist = dist;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy != null)
                turr.SetTarget(closestEnemy);
        }
        else
        {
            turr.SetTarget(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
            if (!enemies.Contains(other.transform))
            {
                enemies.Add(other.transform);
            }
    }
}