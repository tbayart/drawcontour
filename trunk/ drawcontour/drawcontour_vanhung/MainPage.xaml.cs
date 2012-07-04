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
        public enum Options
        {
            move_rectanger,changersize,none
        }
        private Options kiemtra = Options.none;
        public MainPage()
        {
            InitializeComponent();
        }


        List<Ellipse> listelipses = new List<Ellipse>();
        Rectangle shape_selected;
        bool movedown = false;
        double positionx;
        double positiony;
        Point position_shape;
        Ellipse elipse=new Ellipse();
        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movedown = true;
            Point vitricuahinh = new Point();
            vitricuahinh.X =(double) shape_selected.GetValue(Canvas.LeftProperty);
            vitricuahinh.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
            Point currentselected = e.GetPosition(canvas1);
            double khoangcach = distance(vitricuahinh, currentselected);
            if (khoangcach<20)
            {
                kiemtra = Options.changersize;
            }
        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            movedown = false;
            kiemtra = Options.none;
        }
       
        private double distance(Point diem1,Point diem2)
        {
            return Math.Sqrt(Math.Pow(diem2.X - diem1.X, 2) + Math.Pow(diem2.Y - diem1.Y, 2));
        }
       
       
        
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
           
              if (kiemtra==Options.move_rectanger&&movedown==true)
              {
                  
                 
                  Point current_selected = e.GetPosition(canvas1);
                  double x = current_selected.X - position_shape.X;
                  double y = current_selected.Y - position_shape.Y;
                  shape_selected.SetValue(Canvas.LeftProperty, x);
                  shape_selected.SetValue(Canvas.TopProperty, y);
                  positionx=x;
                  positiony=y;
              }
             else if (kiemtra==Options.changersize)
             {
                 Point current = new Point();
                 current.X =(double) shape_selected.GetValue(Canvas.LeftProperty);
                 current.Y =(double) shape_selected.GetValue(Canvas.TopProperty);
                 Point enpoint = e.GetPosition(canvas1);
                 double x = enpoint.X - current.X;
                 double y = enpoint.Y - current.Y;
                 if (shape_selected.Width-x<0||shape_selected.Height-y<0)
                 {
                     return;
                 }
                 shape_selected.Width-=x;
                 shape_selected.Height-=y;
                 elipse.SetValue(Canvas.LeftProperty, enpoint.X-10);
                 elipse.SetValue(Canvas.TopProperty, enpoint.Y-10);
                 shape_selected.SetValue(Canvas.LeftProperty, enpoint.X);
                 shape_selected.SetValue(Canvas.TopProperty, enpoint.Y);
             }
            else
             
                  {
                      if (shape_selected == null)
                      {
                          return;
                      }
                      Point current_selected = e.GetPosition(canvas1);
                      Point vitricuahinh = new Point();
                      vitricuahinh.X = (double)shape_selected.GetValue(Canvas.LeftProperty);
                      vitricuahinh.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
                      double khoangcach = distance(current_selected, vitricuahinh);
                      if (khoangcach < 20)
                      {
                          //kiemtra = Options.changersize;
                          kiemtra = Options.none;
                          elipse.Stroke = new SolidColorBrush(Colors.Red);
                          elipse.StrokeThickness = 1.5;
                          elipse.Width = 20;
                          elipse.Height = 20;
                          elipse.SetValue(Canvas.LeftProperty, vitricuahinh.X - 10);
                          elipse.SetValue(Canvas.TopProperty, vitricuahinh.Y - 10);
                          if (canvas1.Children.Contains(elipse))
                          {
                              elipse.Visibility = Visibility.Visible;
                          }
                          else
                          {
                              canvas1.Children.Add(elipse);
                          }
                      }
                      else
                      {
                          kiemtra = Options.move_rectanger;
                          elipse.Visibility = Visibility.Collapsed;
                      }
                  }
           
             
            
          
        }
      
        private void addrectanger_Click(object sender, RoutedEventArgs e)
        {
            
            Rectangle rectanger = new Rectangle();
            rectanger.Width = 200;
            rectanger.Height = 250;
            rectanger.Stroke = new SolidColorBrush(Colors.Red);
            rectanger.StrokeThickness = 2;
            rectanger.Fill = new SolidColorBrush(Colors.Yellow);
            rectanger.MouseLeftButtonUp += new MouseButtonEventHandler(rectanger_MouseLeftButtonUp);
            rectanger.MouseLeftButtonDown += new MouseButtonEventHandler(rectanger_MouseLeftButtonDown);
            rectanger.MouseLeave += new MouseEventHandler(rectanger_MouseLeave);
           rectanger.MouseEnter+=new MouseEventHandler(rectanger_MouseEnter);
            canvas1.Children.Add(rectanger);
           
        }

        void rectanger_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            kiemtra = Options.none;
        }

        void rectanger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shape_selected = (Rectangle)sender;
            position_shape = e.GetPosition(shape_selected);
            kiemtra = Options.move_rectanger;
            positionx = position_shape.X;
            positiony = position_shape.Y;
        }

        void rectanger_MouseEnter(object sender, MouseEventArgs e)
        {

           
        }

        void rectanger_MouseLeave(object sender, MouseEventArgs e)
        {
           
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
            elipse_topright.SetValue(Canvas.LeftProperty, positionx - 9 + shape_selected.Width);
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
            elipse_belowleft.SetValue(Canvas.TopProperty, positiony - 9 + shape_selected.Height);
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
            elipse_belowright.SetValue(Canvas.LeftProperty, positionx - 9 + shape_selected.Width);
            elipse_belowright.SetValue(Canvas.TopProperty, positiony - 9 + shape_selected.Height);
            canvas1.Children.Add(elipse_belowright);
            listelipses.Add(elipse_belowright);
        }
       

       
        
       

       






       
    }
}
