using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace drawcontour_vanhung
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
       
        Shape shape_curentselected;
        
        Point start;
        Point current;
        Point topleft;
        Point topright;
        Point belowright;
        Point belowleft;
        PointCollection points = new PointCollection();
        List<Ellipse> listelipses = new List<Ellipse>();
        bool changer = false;
       // bool conditionchangersize = false;
       
        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }
       
        private double distance(Point diem1,Point diem2)
        {
            return Math.Sqrt(Math.Pow(diem2.X - diem1.X, 2) + Math.Pow(diem2.Y - diem1.Y, 2));
        }
       
       
        private void ReDraw(Point location,double Width,double Height)
        {
            Rectangle newrectanger = new Rectangle();
            newrectanger.Stroke = new SolidColorBrush(Colors.Red);
            newrectanger.StrokeThickness = 2;
            newrectanger.Fill = new SolidColorBrush(Colors.Yellow);
            newrectanger.Width = Width;
            newrectanger.Height = Height;
            newrectanger.SetValue(Canvas.LeftProperty, location.X);
            newrectanger.SetValue(Canvas.TopProperty, location.Y);
            canvas1.Children.Add(newrectanger);
        }
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
           
              
          
        }
       bool moving_rectanger=false;
       double positionx = 0;
       double positiony;
        private void addrectanger_Click(object sender, RoutedEventArgs e)
        {
            foreach (Ellipse elipse in listelipses)
            {
                canvas1.Children.Remove(elipse);
            }
            Rectangle rectanger = new Rectangle();
            rectanger.Width = 200;
            rectanger.Height = 250;
            rectanger.Stroke = new SolidColorBrush(Colors.Red);
            rectanger.StrokeThickness = 2;
            rectanger.Fill = new SolidColorBrush(Colors.Yellow);
            rectanger.MouseLeftButtonDown += new MouseButtonEventHandler(rectanger_MouseLeftButtonDown);
            rectanger.MouseLeftButtonUp += new MouseButtonEventHandler(rectanger_MouseLeftButtonUp);
            rectanger.MouseMove += new MouseEventHandler(rectanger_MouseMove);
            rectanger.MouseLeave += new MouseEventHandler(rectanger_MouseLeave);
            canvas1.Children.Add(rectanger);
           //positionx= ()rectanger.GetValue(Canvas.LeftProperty);
        }

        void rectanger_MouseLeave(object sender, MouseEventArgs e)
        {
            moving_rectanger = false;
            this.Cursor = Cursors.Arrow;
            //throw new NotImplementedException();
        }
        void vehinhtaidiemtrenbentrai()
        {
            
            Ellipse elipse_toleft = new Ellipse();
            elipse_toleft.Width = 20;
            elipse_toleft.Height = 20;
            //positionx = (int)shape_curentselected.GetValue(Canvas.LeftProperty);
            //positiony = (int)shape_curentselected.GetValue(Canvas.TopProperty);
            elipse_toleft.Stroke = new SolidColorBrush(Colors.Green);
            elipse_toleft.StrokeThickness = 2;
            elipse_toleft.SetValue(Canvas.LeftProperty, positionx-9);
            elipse_toleft.SetValue(Canvas.TopProperty, positiony - 9);
            canvas1.Children.Add(elipse_toleft);
            listelipses.Add(elipse_toleft);
        }
        void vehinhtaidiemtrenbentrai_changer()
        {

            Ellipse elipse_toleft = new Ellipse();
            elipse_toleft.Width = 20;
            elipse_toleft.Height = 20;
            //positionx = (int)shape_curentselected.GetValue(Canvas.LeftProperty);
            //positiony = (int)shape_curentselected.GetValue(Canvas.TopProperty);
            elipse_toleft.Stroke = new SolidColorBrush(Colors.Yellow);
            elipse_toleft.StrokeThickness = 2;
            elipse_toleft.SetValue(Canvas.LeftProperty, positionx - 9);
            elipse_toleft.SetValue(Canvas.TopProperty, positiony - 9);
            canvas1.Children.Add(elipse_toleft);
            listelipses.Add(elipse_toleft);
        }
        void vehinhtaidiembenphaitren()
        {
            
            Ellipse elipse_topright = new Ellipse();
            elipse_topright.Width = 20;
            elipse_topright.Height = 20;
            elipse_topright.Stroke = new SolidColorBrush(Colors.Green);
            elipse_topright.StrokeThickness = 2;
            elipse_topright.SetValue(Canvas.LeftProperty, positionx - 9+shape_curentselected.Width);
            elipse_topright.SetValue(Canvas.TopProperty, positiony - 9);
            canvas1.Children.Add(elipse_topright);
            listelipses.Add(elipse_topright);
        }
        void vehinhtaidiembentraiduoi()
        {
            Ellipse elipse_belowleft = new Ellipse();
            elipse_belowleft.Width = 20;
            elipse_belowleft.Height = 20;
            elipse_belowleft.Stroke = new SolidColorBrush(Colors.Green);
            elipse_belowleft.StrokeThickness = 2;
            elipse_belowleft.SetValue(Canvas.LeftProperty, positionx - 9);
            elipse_belowleft.SetValue(Canvas.TopProperty, positiony - 9+shape_curentselected.Height);
            canvas1.Children.Add(elipse_belowleft);
            listelipses.Add(elipse_belowleft);
        }
        void vehinhtaidiembenphaiduoi()
        {
            Ellipse elipse_belowright = new Ellipse();
            elipse_belowright.Width = 20;
            elipse_belowright.Height = 20;
            elipse_belowright.Stroke = new SolidColorBrush(Colors.Green);
            elipse_belowright.StrokeThickness = 2;
            elipse_belowright.SetValue(Canvas.LeftProperty, positionx - 9+shape_curentselected.Width);
            elipse_belowright.SetValue(Canvas.TopProperty, positiony - 9 + shape_curentselected.Height);
            canvas1.Children.Add(elipse_belowright);
            listelipses.Add(elipse_belowright);
        }
        void rectanger_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (moving_rectanger == true)
                {
                    foreach (Ellipse elipse in listelipses)
                    {
                        canvas1.Children.Remove(elipse);
                    }
                    Canvas canvas = (Canvas)shape_curentselected.Parent;
                    current = e.GetPosition(canvas);
                    double x = current.X - start.X;
                    double y = current.Y - start.Y;
                    shape_curentselected.SetValue(Canvas.LeftProperty, x);
                    shape_curentselected.SetValue(Canvas.TopProperty, y);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            //throw new NotImplementedException();
        }

        void rectanger_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shape shape = (Shape)sender;
            positionx = (double)shape.GetValue(Canvas.LeftProperty);
            positiony = (double)shape.GetValue(Canvas.TopProperty);
            topleft = new Point(positionx, positiony);
            topright = new Point(positionx + shape_curentselected.Width, positiony);
            belowleft = new Point(positionx, positiony + shape_curentselected.Height);
            belowright = new Point(positionx + shape_curentselected.Width, positiony + shape_curentselected.Height);
            vehinhtaidiemtrenbentrai();
            vehinhtaidiembenphaitren();
            vehinhtaidiembenphaiduoi();
           
            vehinhtaidiembentraiduoi();
            
            this.Cursor = Cursors.Arrow;
            moving_rectanger = false;
            //throw new NotImplementedException();
            
        }
        
        void rectanger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           try
           {
               //changer = false;
               shape_curentselected = (Shape)sender;
               FrameworkElement ui = (FrameworkElement)sender;
               foreach (Ellipse elipse in listelipses)
               {
                   canvas1.Children.Remove(elipse);
               }
             positionx= (double) ui.GetValue(Canvas.LeftProperty);
             positiony = (double)ui.GetValue(Canvas.TopProperty);
             //label3.Content = positionx.ToString();
             //label4.Content = positiony.ToString();
               //shape_curentselected = (Shape)sender;
               //shape_curentselected.GetValue(Canvas.LeftProperty);
               //start = e.GetPosition(shape_curentselected);
               topleft = new Point(positionx, positiony);
               topright = new Point(positionx + shape_curentselected.Width,positiony);
               belowleft = new Point(positionx, positiony + shape_curentselected.Height);
               belowright = new Point(positionx + shape_curentselected.Width, positiony + shape_curentselected.Height);
               moving_rectanger = true;
               //vehinhtaidiemtrenbentrai();
               //vehinhtaidiembenphaitren();
               //vehinhtaidiembenphaiduoi();

               //vehinhtaidiembentraiduoi();
               this.Cursor = Cursors.Hand;
           }
           catch (System.Exception ex)
           {
           	
           }
            //throw new NotImplementedException();
        }

        private void canvas1_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;

        }

        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            Point current = e.GetPosition(canvas1);
            double khoang_cach = distance(topleft, current);
            if (khoang_cach<20)
            {
                //MessageBox.Show("");
                changer = true;
                this.Cursor = Cursors.SizeNWSE;
                moving_rectanger = false;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                //changer = false;
                //moving_rectanger = true;
            }
            //label1.Content = e.GetPosition(canvas1).X.ToString();
            //label2.Content = e.GetPosition(canvas1).Y.ToString();
        }
        private void changershape(Point location,double Width,double Height)
        {
            if (changer==true)
            {
                shape_curentselected.SetValue(Canvas.LeftProperty, location.X);
                shape_curentselected.SetValue(Canvas.TopProperty, location.Y);
                shape_curentselected.Width = Width;
                shape_curentselected.Height = Height;
            }
        }
        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (changer==true)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
        }

        private void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (changer==true)
            {
                Point current = e.GetPosition(canvas1);
                double widht = belowright.X - current.X;
                double Height = belowright.Y - current.Y;
                topleft.X = current.X;
                topleft.Y = current.Y;
                changershape(current, widht, Height);
            }
        }
    }
}
