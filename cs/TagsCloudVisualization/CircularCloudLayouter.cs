﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter
    {
        private Point center_cloud;
        public List<Rectangle> rectangles { get; }
        private const double radiusStep = 1;
        private const double angleStep = 1;
        private double angle = 1;

        public CircularCloudLayouter(Point center)
        {
            center_cloud = center;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
                throw new ArgumentException("The dimensions of the rectangle must be greater than or equal to zero");
            var rectangle = GetNextRectangle(rectangleSize);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            var rectangle = GetRectangle(rectangleSize);
            while (CheckIntersect(rectangle))
            {
                rectangle = GetRectangle(rectangleSize);
            }
            return rectangle;
        }

        private bool CheckIntersect(Rectangle rectangle)
        {
            return rectangles.Any(r => r.IntersectsWith(rectangle));
        }

        private Rectangle GetRectangle(Size rectangleSize)
        {
            return new Rectangle(GetNextPoint(), rectangleSize);
        }

        private Point GetNextPoint()
        {
            var x = (int)(Math.Cos(angle) * radiusStep * angle + center_cloud.X);
            var y = (int)(Math.Sin(angle) * radiusStep * angle + center_cloud.Y);
            angle += angleStep / angle;
            return new Point(x, y);
        }
    }
}
