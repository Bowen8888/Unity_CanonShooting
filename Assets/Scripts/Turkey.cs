using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey {
    public List<Point> Points;
    public bool grounded = true;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    private float gravity = -0.01f;
    private float jumpForce = 0.5f;
    private float walkForce = 0.1f;
    private float ground = -7.9f;
    private float bounce = 0.6f;
    private float leftWall = -18.26f;
    private float mountainLeftBorder = -8.62f;

    public Turkey(List<Point> points)
    {
        Points = points;
        for (int i=0; i< points.Count-1; i++)
        {
            Points[i].next = Points[i + 1];
            Points[i].xDistanceToNext = Points[i].next.x - Points[i].x;
            Points[i].yDistanceToNext = Points[i].next.y - Points[i].y;
        }
        UpdateBorders();
    }

    public void UpdateTurkey()
    {
        UpdatePoints();
        ConstrainPoints();
        UpdateSticks();
        UpdateBorders();
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

    public void UpdateBorders()
    {
        for (int i=0; i<Points.Count; i++)
        {
            Point p = Points[i];
            if (i == 0)
            {
                minX = maxX = p.x;
                minY = maxY = p.y;
            }
            else
            {
                minX = Math.Min(minX, p.x);
                maxX = Math.Max(maxX, p.x);
                minY = Math.Min(minY, p.y);
                maxY = Math.Max(maxY, p.y);
            }
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

            if (p.x < leftWall)
            {
                p.x = leftWall;
                p.oldX = p.x + vx;
            }

            if (p.x > mountainLeftBorder)
            {
                p.x = mountainLeftBorder;
                p.oldX = p.x + vx;
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

    public void LateralSlide(bool right)
    {
        foreach (var p in Points)
        {
            float vx = p.x - p.oldX;
            p.oldX = p.x;
            p.x += vx;
            if (right)
            {
                p.x += walkForce;
            }
            else
            {
                p.x -= walkForce;
            }
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