using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey {
    public List<Point> Points;
    public bool grounded = true;
    
    private float gravity = -0.005f;
    private float jumpForce = 0.6f;
    private float ground = -24.97f;
    private float bounce = 0.6f;

    public Turkey(List<Point> points)
    {
        Points = points;
        for (int i=0; i< points.Count-1; i++)
        {
            Points[i].next = Points[i + 1];
            Points[i].xDistanceToNext = Points[i].next.x - Points[i].x;
            Points[i].yDistanceToNext = Points[i].next.y - Points[i].y;
        }
    }

    public void UpdateTurkey()
    {
        UpdatePoints();
        ConstrainPoints();
        UpdateSticks();
    }

    public void UpdatePoints()
    {
        foreach (var p in Points)
        {
            float vx = p.x - p.oldX;
            float vy = p.y - p.oldY;
            p.oldX = p.x;
            p.oldY = p.y;
            p.x += vx;
            p.y += vy;
            p.y += gravity;
        }
    }

    public void ConstrainPoints()
    {
        for (int i=0; i< Points.Count; i++)
        {
            Point p = Points[i];
            float vx = p.x - p.oldX;
            float vy = p.y - p.oldY;

            if (p.y < ground)
            {
                grounded = true;
                p.y = ground;
                p.oldY = p.y + vy * bounce;
            }
        }
    }
    
    public void UpdateSticks()
    {
        for(int i =Points.Count-2; i>=0; i--)
        {
            Points[i].UpdateStick();
        }
    }

    public void TurkeyJump()
    {
        grounded = false;
        foreach (var p in Points)
        {
            float vy = p.y - p.oldY;
            p.oldY = p.y;
            p.y += vy;
            p.y += jumpForce;
        }
    }
}

public class Point
{
    public float x;
    public float y;
    public float oldX;
    public float oldY;
    public Point next;
    public float xDistanceToNext;
    public float yDistanceToNext;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
        oldX = x;
        oldY = y;
    }

    public void UpdateStick()
    {
        float dx = next.x - x;
        float dy = next.y - y;
        float offSetX = xDistanceToNext - dx;
        float offSetY = yDistanceToNext - dy;
        x -= offSetX/2;
        y -= offSetY/2;
        next.x += offSetX / 2;
        next.y += offSetY / 2;
    }
}