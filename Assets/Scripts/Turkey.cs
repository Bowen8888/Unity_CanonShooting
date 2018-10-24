using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey {
    public List<Point> Points;
    public List<Stick> Sticks = new List<Stick>();
    public bool grounded = true;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    private float gravity = -0.05f;
    private float jumpForce = 0.4f;
    private float slightJumpForce = 0.1f;
    private float walkForce = 0.01f;
    private float windOffset = 0.05f;
    private float ground = -7.9f;
    private float bounce = 0.6f;
    private float leftWall = -18.912f;
    private float mountainLeftBorder = -8.944f;
    private float mountainSlideForce = 0.1f;
    private float currentWind = 0;
    private float previousWind = 0;
    private float defaultWalkingForce = 0;

    public Turkey(List<Point> points)
    {
        Points = points;
        for (int i=0; i< points.Count-1; i++)
        {
            Points[i].next = Points[i + 1];
            Points[i].xDistanceToNext = Points[i].next.x - Points[i].x;
            Points[i].yDistanceToNext = Points[i].next.y - Points[i].y;
        }
        
        //body
        Sticks.Add(new Stick(Points[13],Points[14]));
        Sticks.Add(new Stick(Points[14],Points[15]));
        Sticks.Add(new Stick(Points[15],Points[16]));
        Sticks.Add(new Stick(Points[16],Points[17]));
        Sticks.Add(new Stick(Points[17],Points[18]));
        Sticks.Add(new Stick(Points[18],Points[19]));
        Sticks.Add(new Stick(Points[19],Points[20]));
        Sticks.Add(new Stick(Points[20],Points[2]));
        Sticks.Add(new Stick(Points[2],Points[3]));
        Sticks.Add(new Stick(Points[3],Points[13]));
        
        Sticks.Add(new Stick(Points[23],Points[13]));
        Sticks.Add(new Stick(Points[23],Points[14]));
        Sticks.Add(new Stick(Points[23],Points[15]));
        Sticks.Add(new Stick(Points[23],Points[16]));
        Sticks.Add(new Stick(Points[23],Points[17]));
        Sticks.Add(new Stick(Points[23],Points[18]));
        Sticks.Add(new Stick(Points[23],Points[19]));
        Sticks.Add(new Stick(Points[23],Points[20]));
        Sticks.Add(new Stick(Points[23],Points[2]));
        Sticks.Add(new Stick(Points[23],Points[3]));
        
        //neck
        Sticks.Add(new Stick(Points[12],Points[13]));
        Sticks.Add(new Stick(Points[12],Points[3]));
        Sticks.Add(new Stick(Points[12],Points[4]));
        Sticks.Add(new Stick(Points[12],Points[5]));
        Sticks.Add(new Stick(Points[12],Points[11]));
        Sticks.Add(new Stick(Points[3],Points[4]));
        Sticks.Add(new Stick(Points[4],Points[5]));
        Sticks.Add(new Stick(Points[5],Points[11]));
        Sticks.Add(new Stick(Points[11],Points[13]));
        Sticks.Add(new Stick(Points[11],Points[2]));
        
        Sticks.Add(new Stick(Points[13],Points[4]));
        Sticks.Add(new Stick(Points[13],Points[5]));
        Sticks.Add(new Stick(Points[13],Points[7]));
        Sticks.Add(new Stick(Points[13],Points[8]));
        Sticks.Add(new Stick(Points[13],Points[9]));
        Sticks.Add(new Stick(Points[13],Points[10]));

        
        //head
        Sticks.Add(new Stick(Points[9],Points[5]));
        Sticks.Add(new Stick(Points[9],Points[7]));
        Sticks.Add(new Stick(Points[9],Points[8]));
        Sticks.Add(new Stick(Points[9],Points[10]));
        Sticks.Add(new Stick(Points[9],Points[11]));
        Sticks.Add(new Stick(Points[9],Points[6]));
        
        Sticks.Add(new Stick(Points[10],Points[11]));
        Sticks.Add(new Stick(Points[10],Points[4]));
        Sticks.Add(new Stick(Points[10],Points[12]));
        Sticks.Add(new Stick(Points[10],Points[8]));
        Sticks.Add(new Stick(Points[11],Points[5]));
        Sticks.Add(new Stick(Points[7],Points[5]));
        Sticks.Add(new Stick(Points[7],Points[15]));
        Sticks.Add(new Stick(Points[7],Points[16]));
        Sticks.Add(new Stick(Points[7],Points[17]));
        Sticks.Add(new Stick(Points[7],Points[18]));
        Sticks.Add(new Stick(Points[7],Points[19]));
        
        
        //wattle
        Sticks.Add(new Stick(Points[6],Points[5]));
        Sticks.Add(new Stick(Points[6],Points[7]));

        //external
        Sticks.Add(new Stick(Points[4],Points[6]));
        Sticks.Add(new Stick(Points[6],Points[8]));
        Sticks.Add(new Stick(Points[14],Points[11]));
        Sticks.Add(new Stick(Points[8],Points[4]));
        
        //legs
        Sticks.Add(new Stick(Points[3],Points[1]));
        Sticks.Add(new Stick(Points[3],Points[0]));
        Sticks.Add(new Stick(Points[1],Points[20]));
        Sticks.Add(new Stick(Points[1],Points[21]));
        Sticks.Add(new Stick(Points[1],Points[22]));
        Sticks.Add(new Stick(Points[0],Points[22]));
        Sticks.Add(new Stick(Points[19],Points[21]));
        Sticks.Add(new Stick(Points[2],Points[21]));
        Sticks.Add(new Stick(Points[20],Points[22]));
        Sticks.Add(new Stick(Points[2],Points[0]));
        Sticks.Add(new Stick(Points[2],Points[0]));
        Sticks.Add(new Stick(Points[21],Points[0]));
        
        Sticks.Add(new Stick(Points[22],Points[12]));
        Sticks.Add(new Stick(Points[22],Points[13]));
        Sticks.Add(new Stick(Points[22],Points[14]));
        Sticks.Add(new Stick(Points[22],Points[15]));
        Sticks.Add(new Stick(Points[22],Points[16]));
        Sticks.Add(new Stick(Points[22],Points[17]));
        
        Sticks.Add(new Stick(Points[21],Points[12]));
        Sticks.Add(new Stick(Points[21],Points[13]));
        Sticks.Add(new Stick(Points[21],Points[14]));
        Sticks.Add(new Stick(Points[21],Points[15]));
        Sticks.Add(new Stick(Points[21],Points[16]));
        Sticks.Add(new Stick(Points[21],Points[17]));
        
        Sticks.Add(new Stick(Points[0],Points[12]));
        Sticks.Add(new Stick(Points[0],Points[13]));
        Sticks.Add(new Stick(Points[0],Points[14]));
        Sticks.Add(new Stick(Points[0],Points[15]));
        Sticks.Add(new Stick(Points[0],Points[16]));
        Sticks.Add(new Stick(Points[0],Points[17]));
        
        Sticks.Add(new Stick(Points[1],Points[12]));
        Sticks.Add(new Stick(Points[1],Points[13]));
        Sticks.Add(new Stick(Points[1],Points[14]));
        Sticks.Add(new Stick(Points[1],Points[15]));
        Sticks.Add(new Stick(Points[1],Points[16]));
        Sticks.Add(new Stick(Points[1],Points[17]));
        
        
        
        UpdateBorders();
    }

    public void UpdateTurkey(float windApplied, float wf)
    {
        previousWind = currentWind;
        currentWind = windApplied;
        defaultWalkingForce = wf;
        UpdatePoints();
        ConstrainPoints();
        UpdateSticks();
        UpdateBorders();
    }

    public void UpdatePoints()
    {
        for(int i=0; i< Points.Count; i++)
        {
            Point p = Points[i];
            float vx = p.x - p.oldX;
            float vy = p.y - p.oldY;
            p.oldX = p.x;
            p.oldY = p.y;
            p.x += vx-previousWind+currentWind;
            p.y += vy;
            if (i==0 || i==22 || i == 1)
            { 
                p.y += gravity;
            }
            if (i==8)
            { 
                p.x += defaultWalkingForce;
            }
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
        }
    }
    
    public void UpdateSticks()
    {
        for (int i = 0; i < Sticks.Count; i++)
        {
            Stick s = Sticks[i];
            float dx = s.p1.x - s.p0.x;
            float dy = s.p1.y - s.p0.y;
            float distance = (float) Math.Sqrt(dx * dx + dy * dy);
            float difference = s.length - distance;
            float percent = difference / distance / 2;
            float offsetX = dx * percent;
            float offsetY = dy * percent;

            s.p0.x -= offsetX;
            s.p0.y -= offsetY;
            s.p1.x += offsetX;
            s.p1.y += offsetY;
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

    public void SlightJump()
    {
        foreach (var p in Points)
        {
            float vy = p.y - p.oldY;
            p.oldY = p.y;
            p.y += vy;
            p.y += slightJumpForce;
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
    
    public void MountainBouncing(float y)
    {
        for (int i =0; i< Points.Count;i++)
        {
            if (i % 3 == 0)
            {
                Point p = Points[i];
//                p.oldX = p.x;
//                p.x -= mountainSlideForce;
                p.oldY = p.y;
                p.y += mountainSlideForce;
            }
        }
    }

    public void VertexSlide(int index, float ax, float ay)
    {
        Point p = Points[index];
        float vx = p.x - p.oldX;
        float vy = p.y - p.oldY;
        p.oldY = p.y;
        p.y += vy;
        p.y += ay;
        p.oldX = p.x;
        p.x += vx;
        p.x += ax;
        
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

public class Stick
{
    public Point p0;
    public Point p1;
    public float length;

    public Stick(Point p0, Point p1)
    {
        this.p0 = p0;
        this.p1 = p1;
        length = distance(p0, p1);
    }

    private float distance(Point p0, Point p1)
    {
        float dx = p1.x - p0.x;
        float dy = p1.y - p0.y;
        return (float) Math.Sqrt(dx * dx + dy * dy);
    }
}