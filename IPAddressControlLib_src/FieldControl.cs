using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace IPAddressControlLib
{
   internal enum Direction
   {
      Forward,
      Backward
   };

   internal enum Selection
   {
      None,
      All
   };

   internal delegate bool CedeFocusHandler( int fieldId, Direction direction, Selection selection );
   internal delegate void TextChangedHandler( int fieldId, string newText );

	/// <summary>
	/// Summary description for FieldControl.
	/// </summary>
	internal class FieldControl : System.Windows.Forms.TextBox
	{
      public event CedeFocusHandler CedeFocusEvent;
      public event TextChangedHandler TextChangedEvent;

      public const int MinimumValue = 0;
      public const int MaximumValue = 255;

      #region Private Data

      private int _fieldId = -1;

      private bool _invalidKeyDown;

      private int _rangeLower;  // using " = MinimumValue; " here is flagged by FxCop
      private int _rangeUpper = MaximumValue;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      #endregion

      #region Public Properties

      public int FieldId
      {
         get 
         {
            return _fieldId;
         }
         set
         {
            _fieldId = value;
         }
      }

      public bool Blank
      {
         get
         {
            if ( this.Text != null &&
                 this.Text.Length > 0 )
            {
               return false;
            }

            return true;
         }
      }

      public int RangeLower
      {
         get
         {
            return _rangeLower;
         }
         set
         {
            if ( value < MinimumValue )
            {
               _rangeLower = MinimumValue;
            }
            else
            if ( value > _rangeUpper )
            {
               _rangeLower = _rangeUpper;
            }
            else
            {
               _rangeLower = value;
            }

            if ( this.Value < _rangeLower )
            {
               this.Text = _rangeLower.ToString( CultureInfo.InvariantCulture );
            }
         }
      }

      public int RangeUpper
      {
         get
         {
            return _rangeUpper;
         }
         set
         {
            if ( value < _rangeLower )
            {
               _rangeUpper = _rangeLower;
            }
            else
            if ( value > MaximumValue )
            {
               _rangeUpper = MaximumValue;
            }
            else
            {
               _rangeUpper = value;
            }

            if ( this.Value > _rangeUpper )
            {
               this.Text = _rangeUpper.ToString( CultureInfo.InvariantCulture );
            }
         }
      }

      #endregion

      #region Public Functions

      public void SetFont( Font font )
      {
         this.Font = font;
         this.Size = CalculateControlSize();
      }

      public void TakeFocus( Direction direction, Selection selection )
      {
         this.Focus();

         if ( selection == Selection.All )
         {
            this.SelectionStart = 0;
            this.SelectionLength = this.TextLength;
         }
         else
         if ( direction == Direction.Forward )
         {
            this.SelectionStart = 0;
         }
         else
         {
            this.SelectionStart = this.TextLength;
         }
      }

      public override string ToString()
      {
         return Value.ToString( CultureInfo.InvariantCulture );
      }

      #endregion

      #region Constructor

		public FieldControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

         this.AutoSize         = false;
         this.BorderStyle      = BorderStyle.None;
         this.MaxLength        = 3;
         this.TabStop          = false;
         this.TextAlign        = HorizontalAlignment.Center;

         this.Size             = CalculateControlSize();
		}

      #endregion

      #region Overrides

      protected override void OnKeyDown( KeyEventArgs e )
      {
         _invalidKeyDown = false;

         if ( e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9 )
         {
            if ( e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 )
            {
               if ( !ValidKeyDown( e ) )
               {
                  _invalidKeyDown = true;
               }
            }
         }

         if ( ( e.KeyCode == Keys.OemPeriod ||
                e.KeyCode == Keys.Decimal ||
                e.KeyCode == Keys.Space ) &&
                this.TextLength != 0 && 
                this.SelectionLength == 0 &&
                this.SelectionStart != 0  )
         {
            if ( CedeFocusEvent != null )
            {
               CedeFocusEvent( this.FieldId, Direction.Forward, Selection.All );
            }
         }

         if ( e.KeyCode == Keys.Left || e.KeyCode == Keys.Up )
         {
            if ( e.Modifiers == Keys.Control )
            {
               if ( CedeFocusEvent != null )
               {
                  CedeFocusEvent( this.FieldId, Direction.Backward, Selection.All );
               }
            }
            else
            if ( SelectionLength == 0 && SelectionStart == 0 )
            {
               if ( CedeFocusEvent != null )
               {
                  CedeFocusEvent( this.FieldId, Direction.Backward, Selection.None );
               }
            }
         }

         if ( e.KeyCode == Keys.Right || e.KeyCode == Keys.Down )
         {
            if ( e.Modifiers == Keys.Control )
            {
               if ( CedeFocusEvent != null )
               {
                  CedeFocusEvent( this.FieldId, Direction.Forward, Selection.All );
               }
            }
            else
            if ( SelectionLength == 0 && SelectionStart == Text.Length )
            {
               if ( CedeFocusEvent != null )
               {
                  CedeFocusEvent( this.FieldId, Direction.Forward, Selection.None );
               }
            }
         }

         base.OnKeyDown( e );
      }

      protected override void OnKeyPress(KeyPressEventArgs e)
      {
         if ( _invalidKeyDown )
         {
            e.Handled = true;
         }

         base.OnKeyPress( e );
      }

      protected override void OnTextChanged( EventArgs e )
      {
         if ( !this.Blank )
         {
            try
            {
               int val = Int32.Parse( this.Text, CultureInfo.InvariantCulture );

               if ( val > this.RangeUpper )
               {
                  this.Text = this.RangeUpper.ToString( CultureInfo.InvariantCulture );
               }
               else
               {
                  this.Text = val.ToString( CultureInfo.InvariantCulture );
               }
            }
            catch ( ArgumentNullException )
            {
               this.Text = String.Empty;
            }
            catch ( FormatException )
            {
               this.Text = String.Empty;
            }
            catch ( OverflowException )
            {
               this.Text = String.Empty;
            }
         }

         this.SelectionStart = this.TextLength;

         if ( this.TextChangedEvent != null )
         {
            this.TextChangedEvent( this.FieldId, this.Text );
         }

         if ( this.Text.Length == this.MaxLength && this.Focused )
         {
            if ( CedeFocusEvent != null )
            {
               CedeFocusEvent( this.FieldId, Direction.Forward, Selection.All );
            }
         }

         base.OnTextChanged( e );
      }

      protected override void OnValidating( CancelEventArgs e )
      {
         if ( !this.Blank )
         {
            if ( this.Value < RangeLower )
            {
               this.Text = RangeLower.ToString( CultureInfo.InvariantCulture );
            }
         }

         base.OnValidating( e );
      }

      #endregion

      #region Private Functions

      private Size CalculateControlSize()
      {
         string text = new string( '0', MaxLength );
         return Utility.CalculateStringSize( this.Handle, this.Font, text );
      }

      private static bool ValidKeyDown( KeyEventArgs e )
      {
         if ( e.KeyCode == Keys.Back || 
              e.KeyCode == Keys.Delete )
         {
            return true;
         }
         else
            if ( e.Modifiers == Keys.Control &&
                 ( e.KeyCode == Keys.C ||
                   e.KeyCode == Keys.V ||
                   e.KeyCode == Keys.X ) )
         {
            return true;
         }

         return false;
      }

      #endregion

      #region Private Properties

      private int Value
      {
         get
         {
            try
            {
               return Int32.Parse( this.Text, CultureInfo.InvariantCulture );
            }
            catch ( ArgumentNullException )
            {
               return RangeLower;
            }
            catch ( FormatException )
            {
               return RangeLower;
            }
            catch ( OverflowException )
            {
               return RangeLower;
            }
         }
      }

      #endregion

      #region Generated Code

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         // 
         // FieldControl
         // 
         this.Name = "FieldControl";
      }
		#endregion

      #endregion
	}
}
