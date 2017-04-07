using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuralNetworkWF
{
	public struct Graph
	{
		public PointF[] _ptf;
		public Point[] _pti;
		public Graph(float[] x, float[] y)
		{
			int size = Math.Min(x.Length, y.Length);
			_ptf = new PointF[size];
			_pti = new Point[size];
			for (int i = 0; i < size; ++i)
			{
				_ptf[i] = new PointF(x[i], y[i]);
			}
		}

		public Graph(PointF[] ptf)
		{
			_ptf = ptf;
			_pti = new Point[ptf.Length];
		}
	}
	public partial class GraphBuilder : Control
	{
		List<Graph>		_g;
		//PointF[]	_ptf;
		//Point[]		_pti;

		float[]	_xdelf;
		float[]	_ydelf;
		int[]	_xdeli;
		int[]	_ydeli;
		int _xzerodelind;
		int _yzerodelind;
		float	_xmin, _xmax, _xscale;
		float   _ymin, _ymax, _yscale;
		

		int     _leftmargin, _rightmargin, _topmargin, _bottommargin;
		public Rectangle _gRect;


		SolidBrush  _outterBgBrush, _innerBgBrush;
		SolidBrush  _scaleBrush;
		Pen         _outterBorderPen, _innerBorderPen;
		Pen         _orthoPen, _axisPen;
		List<Pen>	_graphPen;

        object[] _xdelo;
        int _xdelstep;

		public GraphBuilder()
		{
			InitializeComponent();

			_outterBgBrush	= new SolidBrush(Color.LightGray);
			_innerBgBrush	= new SolidBrush(Color.White);
			_scaleBrush = new SolidBrush(Color.Black);

			_outterBorderPen	= new Pen(Color.Black, 1f);
			_innerBorderPen		= new Pen(Color.Black, 1f);
			_orthoPen = new Pen(Color.Gray);
			_axisPen = new Pen(Color.Black, 3f);
			_graphPen = new List<Pen>();

			_leftmargin = 40;
			_rightmargin = 20;
			_topmargin = 20;
			_bottommargin = 20;
			_g = new List<Graph>();
			
			//_ptf = new PointF[0];
			//_pti = new Point[0];
			//SetPoints(new float[] { 0.5f, -0.5f, 0.0f});
			/*_ptf = new PointF[3];
			_ptf[0] = new PointF(0f, 0.5f);
			_ptf[1] = new PointF(1f, -0.5f);
			_ptf[2] = new PointF(2f, 0.0f);
			_pti = new Point[3];*/
			//UpdateGraphLocation();
			SetIntervals(-1.0f, 1.0f, 0.1f, -1f, 1f, 0.2f);
			MinimumSize = new Size(100, 100);
            _xdelo = null;
            _xdelstep = 1;
            //UpdatePositions();
        }
        public void Clear()
		{
			_g.Clear();
			_graphPen.Clear();
            SetIntervals(-1.0f, 1.0f, 0.1f, -1f, 1f, 0.2f);
            _xdelo = null;
        }
        protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics    gfx = pe.Graphics;
			Rectangle   cr  = ClientRectangle;
			Rectangle   gr   = _gRect;
			int crborder = Convert.ToInt32(_outterBorderPen.Width);
			int grborder = Convert.ToInt32(_innerBorderPen.Width);
			// Внешняя рамка
			//cr.Width -= crborder; cr.Height -= crborder;
			cr.Offset(crborder / 2, crborder / 2);
			cr.Width -= crborder; cr.Height -= crborder;
			gfx.DrawRectangle(_outterBorderPen, cr);

			// Внешний фон
			cr.Offset(crborder - crborder / 2, crborder - crborder / 2);
			cr.Width -= crborder; cr.Height -= crborder;
			gfx.FillRectangle(_outterBgBrush, cr);

			// Внутренняя рамка
			gr.Offset(grborder / 2, grborder / 2);
			gr.Width -= grborder; gr.Height -= grborder;
			gfx.DrawRectangle(_innerBorderPen, gr);

			// Внутренний фон
			gr.Offset(grborder - grborder / 2, grborder - grborder / 2);
			gr.Width -= grborder; gr.Height -= grborder;
			gfx.FillRectangle(_innerBgBrush, gr);

			// Сетка, шкалы и оси
			float fntSize = 12.0f;
			Font fnt = new Font("Verdana", fntSize, FontStyle.Regular, GraphicsUnit.Pixel);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < _xdeli.Length; i += _xdelstep)
            {
                gfx.DrawLine(_orthoPen, _xdeli[i], gr.Y, _xdeli[i], gr.Y + gr.Height - 1);
                if (_xdelo != null)
                {
                    if (i < _xdelo.Length)
                    {
                        gfx.DrawString(_xdelo[i].ToString(), fnt, _scaleBrush, _xdeli[i], gr.Y + gr.Height + Convert.ToInt32(fntSize * 0.7) + grborder, sf);
                    }
                }
                else
                {
                    gfx.DrawString(_xdelf[i].ToString(), fnt, _scaleBrush, _xdeli[i], gr.Y + gr.Height + Convert.ToInt32(fntSize * 0.7) + grborder, sf);
                }
            }
			sf.Alignment = StringAlignment.Far;
			for (int i = 0; i < _ydeli.Length; ++i)
			{
				gfx.DrawLine(_orthoPen, gr.X, _ydeli[i], gr.X + gr.Width - 1, _ydeli[i]);
				RectangleF rf = new RectangleF((float)cr.X, _ydeli[i] - fntSize / 2, (float)_leftmargin, fntSize);
				gfx.DrawString(_ydelf[i].ToString(), fnt, _scaleBrush, rf, sf);
			}
			if (_xzerodelind != -1)
				gfx.DrawLine(_axisPen, _xdeli[_xzerodelind], gr.Y, _xdeli[_xzerodelind], gr.Y + gr.Height - 1);
			if (_yzerodelind != -1)
				gfx.DrawLine(_axisPen, gr.X, _ydeli[_yzerodelind], gr.X + gr.Width - 1, _ydeli[_yzerodelind]);

			// Графики
			for (int i = 0; i < _g.Count; ++i)
			{
				for (int pt = 0; pt < _g[i]._pti.Length - 1; ++pt)
				{
					gfx.DrawLine(_graphPen[i], _g[i]._pti[pt], _g[i]._pti[pt + 1]);
                    if (i == 0) gfx.DrawPolygon(_graphPen[i], new Point[] { new Point(_g[i]._pti[pt].X - 2, _g[i]._pti[pt].Y - 2), new Point(_g[i]._pti[pt].X, _g[i]._pti[pt].Y + 2), new Point(_g[i]._pti[pt].X + 2, _g[i]._pti[pt].Y - 2) });
                    else if (i == 1) gfx.DrawRectangle(_graphPen[i], _g[i]._pti[pt].X - 2, _g[i]._pti[pt].Y - 2, 4, 4);
				}
			}


		}
		
		
		private void UpdatePositions()
		{
			int border = Convert.ToInt32(_innerBorderPen.Width);
			float xMult = (_gRect.Width - 2*border - 1) /(_xmax - _xmin);
			float yMult = (_gRect.Height - 2*border - 1)/(_ymax - _ymin);

			for (int g = 0; g < _g.Count; ++g)
			{
				for (int pt = 0; pt < _g[g]._ptf.Length; ++pt)
				{
					_g[g]._pti[pt].X = _gRect.X + Convert.ToInt32((_g[g]._ptf[pt].X - _xmin) * xMult) + border;
					_g[g]._pti[pt].Y = _gRect.Y + _gRect.Height - Convert.ToInt32((_g[g]._ptf[pt].Y - _ymin) * yMult) - border;
				}
			}

			int xDelsBelow0 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_xmin/_xscale)));
			int xDelsAbove0 = Convert.ToInt32(Math.Floor(Convert.ToDouble(_xmax/_xscale)));
			int yDelsBelow0 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_ymin/_yscale)));
			int yDelsAbove0 = Convert.ToInt32(Math.Floor(Convert.ToDouble(_ymax/_yscale)));
			_xdeli = new int[xDelsAbove0 - xDelsBelow0 + 1];
			_xdelf = new float[_xdeli.Length];
			int xdelit = 0;
			_xzerodelind = -1;
			for (int i = xDelsBelow0; i <= xDelsAbove0; ++i)
			{
				float x = _xscale * i;
				_xdelf[xdelit] = x;
				_xdeli[xdelit] = _gRect.X + Convert.ToInt32((x - _xmin) * xMult) + border;
				if (i == 0)
					_xzerodelind = xdelit;
				xdelit++;
			}
			_ydeli = new int[yDelsAbove0 - yDelsBelow0 + 1];
			_ydelf = new float[_ydeli.Length];
			int ydelit = 0;
			_yzerodelind = -1;
			for (int i = yDelsBelow0; i <= yDelsAbove0; ++i)
			{
				float y = _yscale * i;
				_ydelf[ydelit] = y;
				_ydeli[ydelit] = _gRect.Y + _gRect.Height - Convert.ToInt32((y - _ymin) * yMult) - border - 1;
				if (i == 0)
					_yzerodelind = ydelit;
				ydelit++;
			}
			Invalidate();
		}

		private void UpdateGraphLocation()
		{
			int border = Convert.ToInt32(_outterBorderPen.Width);
			_gRect = new Rectangle(_leftmargin + border, _topmargin + border,
				   ClientRectangle.Width - _leftmargin - _rightmargin - 2 * border,
				   ClientRectangle.Height - _topmargin - _bottommargin - 2 * border);
		}

		public void SetIntervals(float xmin, float xmax, float xscale, float ymin, float ymax, float yscale)
		{
			_xmin = xmin;
			_xmax = xmax;
			_xscale = xscale;
			_ymin = ymin;
			_ymax = ymax;
			_yscale = yscale;
			
		}

        public void SetDelimeters(object[] xdels, int xdelstep)
        {
            _xdelo = xdels.ToArray();
            _xdelstep = xdelstep;
        }

		public void AddGraph(Graph g, Pen pen)
		{
			_g.Add(g);
			_graphPen.Add(pen);
			UpdatePositions();
		}
		public void AddGraph(PointF[] ptf, Pen pen)
		{
			_g.Add(new Graph(ptf));
			_graphPen.Add(pen);
			UpdatePositions();
		}
		public void AddGraph(float[] x, float[] y, Pen pen)
		{
			int size = Math.Min(x.Length, y.Length);
			PointF[] ptf = new PointF[size];
			for (int i = 0; i < size; ++i)
			{
				ptf[i].X = x[i];
				ptf[i].Y = y[i];
			}
			_g.Add(new Graph(ptf));
			_graphPen.Add(pen);
			UpdatePositions();
		}
		public void AddGraph(float[] y, Pen pen)
		{
			int size = y.Length;
			PointF[] ptf = new PointF[size];
			for (int i = 0; i < size; ++i)
			{
				ptf[i].X = i;
				ptf[i].Y = y[i];
			}
			_g.Add(new Graph(ptf));
			_graphPen.Add(pen);
			UpdatePositions();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			UpdateGraphLocation();
			UpdatePositions();
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{

		}
	}
}
