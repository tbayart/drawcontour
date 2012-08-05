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
        public enum vitricudiemthaydoi
        {
            topleft,between_top_below_left,none
        }
        private Options kiemtra = Options.none;
        private vitricudiemthaydoi PointPosition_changer = vitricudiemthaydoi.none;
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
            //kiemtra = Options.move_rectanger;
            movedown = true;
            Point vitricuahinh = new Point();
           
            //double khoangcachsovoidiemogiua=distance(vitridiemogiua,cu)
            vitricuahinh.X =(double) shape_selected.GetValue(Canvas.LeftProperty);
            vitricuahinh.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
            Point currentselected = e.GetPosition(canvas1);
            Point vitridiemogiua = new Point();
            vitridiemogiua.X = vitricuahinh.X;
            vitridiemogiua.Y = vitricuahinh.Y + 0.5 * shape_selected.Height;
            
            double khoangcachsovoidiemogiua = distance(vitridiemogiua, currentselected);
            double khoangcach = distance(vitricuahinh, currentselected);
            if (khoangcach<20)
            {
                kiemtra = Options.changersize;
                PointPosition_changer = vitricudiemthaydoi.topleft;
            }
            else if (khoangcachsovoidiemogiua < 40)
            {
                kiemtra = Options.changersize;
                PointPosition_changer = vitricudiemthaydoi.between_top_below_left;
            }
            //else
            //{
            //    kiemtra = Options.none;
            //    PointPosition_changer = vitricudiemthaydoi.none;
            //}
        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            movedown = false;
           // kiemtra = Options.none;
            Point current = new Point();
            current.X = (double)shape_selected.GetValue(Canvas.LeftProperty);
            current.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
            positionx = current.X;
            positiony = current.Y;
            vehinhtaidiembenphaiduoi();
            vehinhtaidiembenphaitren();
            vehinhtaidiembentraiduoi();
            vehinhtaidiemtrenbentrai();
            vehinhogiua();
        }
       
        private double distance(Point diem1,Point diem2)
        {
            return Math.Sqrt(Math.Pow(diem2.X - diem1.X, 2) + Math.Pow(diem2.Y - diem1.Y, 2));
        }
       
       
        
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
           
              if (kiemtra==Options.move_rectanger&&movedown==true)
              {
                  //shape_selected = (Rectangle)sender;
                 foreach (Ellipse elip in listelipses)
                 {
                     canvas1.Children.Remove(elip);
                 }
                  Point current_selected = e.GetPosition(canvas1);
                  double x = current_selected.X - position_shape.X;
                  double y = current_selected.Y - position_shape.Y;
                  shape_selected.SetValue(Canvas.LeftProperty, x);
                  shape_selected.SetValue(Canvas.TopProperty, y);
                  positionx=x;
                  positiony=y;
                  //vehinhogiua();
                  //vehinhtaidiembenphaiduoi();
                  //vehinhtaidiembenphaitren();
                  //vehinhtaidiembentraiduoi();
                  //vehinhtaidiemtrenbentrai();
              }
             else if (kiemtra==Options.changersize&&movedown==true)
             {
                switch (PointPosition_changer)
                {
                    case vitricudiemthaydoi.none:
                        break;
                    case vitricudiemthaydoi.topleft:
                        {
                            Point current = new Point();
                            current.X = (double)shape_selected.GetValue(Canvas.LeftProperty);
                            current.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
                            Point enpoint = e.GetPosition(canvas1);
                            double x = enpoint.X - current.X;
                            double y = enpoint.Y - current.Y;
                            if (shape_selected.Width - x < 3 || shape_selected.Height - y < 3)
                            {
                                return;
                            }
                            foreach (Ellipse elip in listelipses)
                            {
                                canvas1.Children.Remove(elip);
                            }
                            shape_selected.Width -= x;
                            shape_selected.Height -= y;

                            elipse.SetValue(Canvas.LeftProperty, enpoint.X - 10);
                            elipse.SetValue(Canvas.TopProperty, enpoint.Y-10);
                            shape_selected.SetValue(Canvas.LeftProperty, enpoint.X);
                            shape_selected.SetValue(Canvas.TopProperty, enpoint.Y);
                            positionx = enpoint.X;
                            positiony = enpoint.Y;
                            vehinhtaidiembentraiduoi();
                            vehinhtaidiembenphaiduoi();
                            vehinhtaidiemtrenbentrai();
                            vehinhtaidiembenphaitren();
                            vehinhogiua();
                        }
                        break;
                    case vitricudiemthaydoi.between_top_below_left:
                        {
                            Point current = new Point();
                            current.X = (double)shape_selected.GetValue(Canvas.LeftProperty);
                            current.Y = (double)shape_selected.GetValue(Canvas.TopProperty);
                            Point enpoint = e.GetPosition(canvas1);
                            Point between = new Point(current.X, current.Y + 0.5 * shape_selected.Height);
                            double x = enpoint.X - between.X;
                            double y = enpoint.Y - between.Y;
                            if (shape_selected.Width - x < 3 || y>0)
                            {
                               // kiemtra = Options.none;
                                return;
                            }
                            foreach (Ellipse elip in listelipses)
                            {
                                canvas1.Children.Remove(elip);
                            }
                            shape_selected.Width -= x;
                            //shape_selected.Height=shape_selected.Height;
                            
                           
                            shape_selected.SetValue(Canvas.LeftProperty, enpoint.X);
                            //shape_selected.SetValue(Canvas.TopProperty, enpoint.Y-shape_selected.Height*0.5);
                            
                            positionx = enpoint.X;
                            positiony = positiony;
                            //positiony = enpoint.Y-shape_selected.Height*0.5;
                            vehinhtaidiembentraiduoi();
                            vehinhtaidiembenphaiduoi();
                            vehinhtaidiemtrenbentrai();
                            vehinhtaidiembenphaitren();
                            vehinhogiua();
                        }
                        break;
                }
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
                      Point vitridiemogiua = new Point();
                      vitridiemogiua.X = vitricuahinh.X;
                      vitridiemogiua.Y = vitricuahinh.Y + 0.5 * shape_selected.Height;

                      double khoangcachsovoidiemogiua = distance(vitridiemogiua, current_selected);
                      double khoangcach = distance(current_selected, vitricuahinh);
                      if (khoangcach < 20)
                      {
                          //kiemtra = Options.changersize;
                          canvas1.Children.Remove(elipse);
                          PointPosition_changer = vitricudiemthaydoi.topleft;
                          //kiemtra = Options.move_rectanger;
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
                      if (khoangcachsovoidiemogiua < 20)
                      {
                          canvas1.Children.Remove(elipse);
                          PointPosition_changer = vitricudiemthaydoi.between_top_below_left;
                          kiemtra = Options.changersize;

                          elipse.Stroke = new SolidColorBrush(Colors.Red);
                          elipse.StrokeThickness = 1.5;
                          elipse.Width = 20;
                          elipse.Height = 20;
                          elipse.SetValue(Canvas.LeftProperty, vitridiemogiua.X - 10);
                          elipse.SetValue(Canvas.TopProperty, vitridiemogiua.Y - 10);
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
                          kiemtra = Options.none;
                          PointPosition_changer = vitricudiemthaydoi.none;
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
            //kiemtra = Options.none;
            //PointPosition_changer = vitricudiemthaydoi.none;
            shape_selected = (Rectangle)sender;
            Point current = new Point();
            current.X = (double)shape_selected.GetValue(Canvas.LeftProperty);
            current.Y =(double) shape_selected.GetValue(Canvas.TopProperty);
            positionx = current.X;
            positiony = current.Y;

            vehinhtaidiembenphaiduoi();
            vehinhtaidiembenphaitren();
            vehinhtaidiembentraiduoi();
            vehinhtaidiemtrenbentrai();
            vehinhogiua();
        }

        void rectanger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shape_selected = (Rectangle)sender;
          
           
           
           {
              
               if (shape_selected == null)
               {
                   return;
               }
              
               
                   foreach (Ellipse elip in listelipses)
                   {
                       canvas1.Children.Remove(elip);
                   }
                   shape_selected = (Rectangle)sender;
                   position_shape = e.GetPosition(shape_selected);
                   kiemtra = Options.move_rectanger;
                   positionx = (double)shape_selected.GetValue(Canvas.LeftProperty);

                   positiony = (double)shape_selected.GetValue(Canvas.TopProperty);
              
               vehinhtaidiembenphaiduoi();
               vehinhtaidiembenphaitren();
               vehinhtaidiembentraiduoi();
               vehinhtaidiemtrenbentrai();
               vehinhogiua();
           
           }
           if (kiemtra==Options.changersize)
           {
               return;
           }

            
        }

        void rectanger_MouseEnter(object sender, MouseEventArgs e)
        {

            //kiemtra = Options.changersize;
        }

        void rectanger_MouseLeave(object sender, MouseEventArgs e)
        {
            //kiemtra = Options.none;
           // kiemtra = Options.changersize;
        }
        void vehinhogiua()
        {
            Ellipse elipsebetween = new Ellipse();
           // elipsebetween.Name = "elipsebetween";
            elipsebetween.Width = 20;
            elipsebetween.Height = 20;
            elipsebetween.Stroke = new SolidColorBrush(Colors.Gray);
            elipsebetween.StrokeThickness = 2;
            elipsebetween.SetValue(Canvas.LeftProperty, positionx-10);
            elipsebetween.SetValue(Canvas.TopProperty, positiony + 0.5*shape_selected.Height - 10);
            if (listelipses.Contains(elipsebetween)==false)
            {
                listelipses.Add(elipsebetween);
            }
            canvas1.Children.Add(elipsebetween);
        }
        void vehinhtaidiemtrenbentrai()
        {
            
            Ellipse elipse_toleft = new Ellipse();
            elipse_toleft.Width = 20;
            elipse_toleft.Height = 20;
           // elipse_toleft.Name = "elipse_toleft";
            //positionx = (int)shape_curentselected.GetValue(Canvas.LeftProperty);
            //positiony = (int)shape_curentselected.GetValue(Canvas.TopProperty);
            elipse_toleft.Stroke = new SolidColorBrush(Colors.Green);
            elipse_toleft.StrokeThickness = 1.5;
            elipse_toleft.SetValue(Canvas.LeftProperty, positionx-10);
            elipse_toleft.SetValue(Canvas.TopProperty, positiony - 10);
            canvas1.Children.Add(elipse_toleft);
            if (listelipses.Contains(elipse_toleft)==false)
            {
                listelipses.Add(elipse_toleft);
            }
        }
        void vehinhtaidiemtrenbentrai_changer()
        {

            Ellipse elipse_toleft = new Ellipse();
            elipse_toleft.Width = 20;
            elipse_toleft.Height = 20;
            //elipse_toleft.Name = "elipse_toleft";
            //positionx = (int)shape_curentselected.GetValue(Canvas.LeftProperty);
            //positiony = (int)shape_curentselected.GetValue(Canvas.TopProperty);
            elipse_toleft.Stroke = new SolidColorBrush(Colors.Yellow);
            elipse_toleft.StrokeThickness = 1.5;
            elipse_toleft.SetValue(Canvas.LeftProperty, positionx - 10);
            elipse_toleft.SetValue(Canvas.TopProperty, positiony - 10);
            canvas1.Children.Add(elipse_toleft);
            if (listelipses.Contains(elipse_toleft)==false)
            {
                listelipses.Add(elipse_toleft);
            }
        }
        void vehinhtaidiembenphaitren()
        {
            
            Ellipse elipse_topright = new Ellipse();
            elipse_topright.Width = 20;
           // elipse_topright.Name = "elipse_topright";
            elipse_topright.Height = 20;
            elipse_topright.Stroke = new SolidColorBrush(Colors.Green);
            elipse_topright.StrokeThickness = 1.5;
            elipse_topright.SetValue(Canvas.LeftProperty, positionx - 10 + shape_selected.Width);
            elipse_topright.SetValue(Canvas.TopProperty, positiony - 10);
            canvas1.Children.Add(elipse_topright);
            if (listelipses.Contains(elipse_topright) == false)
            {
                listelipses.Add(elipse_topright);
            }
           // listelipses.Add(elipse_topright);
        }
        void vehinhtaidiembentraiduoi()
        {
            Ellipse elipse_belowleft = new Ellipse();
            elipse_belowleft.Width = 20;
            elipse_belowleft.Height = 20;
           // elipse_belowleft.Name = "elipse_belowleft";
            elipse_belowleft.Stroke = new SolidColorBrush(Colors.Green);
            elipse_belowleft.StrokeThickness = 1.5;
            elipse_belowleft.SetValue(Canvas.LeftProperty, positionx - 10);
            elipse_belowleft.SetValue(Canvas.TopProperty, positiony - 10 + shape_selected.Height);
            canvas1.Children.Add(elipse_belowleft);
            if (listelipses.Contains(elipse_belowleft) == false)
            {
                listelipses.Add(elipse_belowleft);
            }
        }
        void vehinhtaidiembenphaiduoi()
        {
            Ellipse elipse_belowright = new Ellipse();
            elipse_belowright.Width = 20;
           // elipse_belowright.Name = "elipse_belowright";
            elipse_belowright.Height = 20;
            elipse_belowright.Stroke = new SolidColorBrush(Colors.Green);
            elipse_belowright.StrokeThickness = 2;
            elipse_belowright.SetValue(Canvas.LeftProperty, positionx - 10 + shape_selected.Width);
            elipse_belowright.SetValue(Canvas.TopProperty, positiony - 10 + shape_selected.Height);
            canvas1.Children.Add(elipse_belowright);
            if (listelipses.Contains(elipse_belowright) == false)
            {
                listelipses.Add(elipse_belowright);
            }
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
       

       
        
       

       






       
    }
}
