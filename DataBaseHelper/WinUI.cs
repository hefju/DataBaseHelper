using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseHelper
{
    class WinUI
    {
        public static void SetButtonImage(Button btn,string bg)
        {
            var path = @"\images\";
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            //var image = Image.FromFile(path+bg + "0");
            List<Image> imgs = new List<Image>();
          //  imgs.AddRange(new Image[] { Image.FromFile(@"\images\" + bg + "0.png"), Image.FromFile(path + bg + "1.png"), Image.FromFile(path + bg + "2.png") });
            imgs.Add(Image.FromFile(basepath+@"\images\btnblue0.png"));
            imgs.Add(Image.FromFile(basepath + @"\images\btnblue1.png"));
            imgs.Add(Image.FromFile(basepath + @"\images\btnblue2.png"));

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.BackgroundImage = imgs[0];//Image.FromFile
            btn.BackgroundImageLayout = ImageLayout.Stretch;

            btn.MouseEnter += (s, e) => { btn.BackgroundImage = imgs[1]; };
            btn.MouseLeave += (s, e) => { btn.BackgroundImage = imgs[0]; };
            btn.MouseDown += (s, e) => { btn.BackgroundImage = imgs[2]; };
            btn.MouseUp += (s, e) => { btn.BackgroundImage = imgs[0]; };
        }



        internal static void SetButtonImage(PictureBox pb, string p)
        {
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            //var image = Image.FromFile(path+bg + "0");
            List<Image> imgs = new List<Image>();
            //  imgs.AddRange(new Image[] { Image.FromFile(@"\images\" + bg + "0.png"), Image.FromFile(path + bg + "1.png"), Image.FromFile(path + bg + "2.png") });
            imgs.Add(Image.FromFile(basepath + @"\images\btnblue0.png"));
            imgs.Add(Image.FromFile(basepath + @"\images\btnblue1.png"));
            imgs.Add(Image.FromFile(basepath + @"\images\btnblue2.png"));

            //btn.FlatStyle = FlatStyle.Flat;
            //btn.FlatAppearance.BorderSize = 0;

            pb.BackgroundImage = imgs[0];//Image.FromFile
            pb.BackgroundImageLayout = ImageLayout.Stretch;

            pb.MouseEnter += (s, e) => { pb.BackgroundImage = imgs[1]; };
            pb.MouseLeave += (s, e) => { pb.BackgroundImage = imgs[0]; };
            pb.MouseDown += (s, e) => { pb.BackgroundImage = imgs[2]; };
            pb.MouseUp += (s, e) => { pb.BackgroundImage = imgs[0]; };
        }

        internal static void SetLabelImage(Label lbl, List<Image> imgs)
        {
            lbl.BorderStyle = BorderStyle.None;
            lbl.BackgroundImage = imgs[0];
            lbl.BackgroundImageLayout = ImageLayout.Stretch;

            lbl.MouseEnter += (s, e) => { lbl.BackgroundImage = imgs[1]; };
            lbl.MouseLeave += (s, e) => { lbl.BackgroundImage = imgs[0]; };
            lbl.MouseDown += (s, e) => { lbl.BackgroundImage = imgs[2]; };
            lbl.MouseUp += (s, e) => { lbl.BackgroundImage = imgs[0]; };
        }
    }
}
