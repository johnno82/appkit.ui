namespace AdMaiora.AppKit.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using Android.Graphics.Drawables;
    using Android.Graphics;
    using Android.Content.Res;

    public static class ButtonExtension
    {
        public static void SetAutomaticPressedState(this Button button)
        {            
            if (button.Background == null)
            {
                ColorStateList cl = button.TextColors;
                if (!cl.IsStateful)
                {
                    Color c = new Color(cl.DefaultColor);

                    Color normalColor = new Color(c.ToArgb());

                    Color pressedColor = new Color(c.ToArgb());
                    pressedColor.R = (byte)(c.R * 1.15f);
                    pressedColor.G = (byte)(c.G * 1.15f);
                    pressedColor.B = (byte)(c.B * 1.15f);

                    Color disabledColor = new Color(c.ToArgb());
                    byte gray = (byte)((c.R * 0.299f) + (c.G * 0.587f) + (c.B * 0.114f));
                    disabledColor.R = gray;
                    disabledColor.G = gray;
                    disabledColor.B = gray;

                    var ncl = new ColorStateList(
                        new[]
                        {
                            new[] { Android.Resource.Attribute.StatePressed },
                            new[] { -Android.Resource.Attribute.StateEnabled },
                            new[] { Android.Resource.Attribute.StateEnabled }

                        },
                        new int[]
                        {
                            pressedColor,
                            disabledColor,
                            normalColor
                        });

                    button.SetTextColor(ncl);
                }

            }
            else
            {
                Drawable normalDrawable = button.Background;                
                Drawable pressedDrawable = null;
                Drawable disableDrawable = null;

                if (normalDrawable is ColorDrawable)
                {
                    Color c = ((normalDrawable) as ColorDrawable).Color;

                    pressedDrawable = new ColorDrawable(c);
                    pressedDrawable.SetColorFilter(Color.White, PorterDuff.Mode.SrcAtop);

                    disableDrawable = new ColorDrawable(c);
                    disableDrawable.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcAtop);
                }
                else if (normalDrawable is BitmapDrawable)
                {
                    BitmapDrawable bd = normalDrawable as BitmapDrawable;

                    pressedDrawable = new BitmapDrawable(bd.Bitmap);
                    pressedDrawable.SetColorFilter(new Color(255, 255, 255, 70), PorterDuff.Mode.SrcAtop);

                    disableDrawable = new BitmapDrawable(bd.Bitmap);
                    disableDrawable.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcAtop);
                }

                StateListDrawable sld = new StateListDrawable();
                sld.AddState(new[] { Android.Resource.Attribute.StatePressed }, pressedDrawable);
                sld.AddState(new[] { -Android.Resource.Attribute.StateEnabled }, disableDrawable);
                sld.AddState(new[] { Android.Resource.Attribute.StateEnabled }, normalDrawable);                

                button.Background = sld;
            }
        }

        public static void SetTextUnderline(this Button button, bool underline)
        {
            if (underline)
                button.PaintFlags |= PaintFlags.UnderlineText;
            else
                button.PaintFlags &= ~PaintFlags.UnderlineText;
        }
    }
}