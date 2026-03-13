using System;
using System.Drawing;

namespace IPAddressControlLib
{
   internal class Utility
   {
      private Utility()
      {
      }

      public static Size CalculateStringSize( IntPtr handle, Font font, string text )
      {
         StringFormat stringFormat = new StringFormat();
         RectangleF rect = new RectangleF( 0, 0, 9999, 9999 );

         CharacterRange[] ranges = { new CharacterRange( 0, text.Length ) };

         Region[] regions = new Region[1];

         stringFormat.SetMeasurableCharacterRanges( ranges );

         Graphics g = Graphics.FromHwnd( handle );

         regions = g.MeasureCharacterRanges( text,
            font, rect, stringFormat );

         rect = regions[0].GetBounds( g );

         float fudgeFactor = ( font.SizeInPoints / 8.25F ) * 3.0F;

         return new Size( (int)(rect.Width + fudgeFactor), (int)(rect.Height) );
      }
   }
}
