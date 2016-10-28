//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright � 2006  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

#region Using directives

using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace ZedGraph
{
  /// <summary>
  ///   Encapsulates a Japanese CandleStick curve type that displays a vertical (or horizontal)
  ///   line displaying the range of data values at each sample point, plus a filled bar
  ///   signifying the opening and closing value for the sample.
  /// </summary>
  /// <remarks>
  ///   For this type to work properly, your <see cref="IPointList" /> must contain
  ///   <see cref="StockPt" /> objects, rather than ordinary <see cref="PointPair" /> types.
  ///   This is because the <see cref="OHLCBarItem" /> type actually displays 5 data values
  ///   but the <see cref="PointPair" /> only stores 3 data values.  The <see cref="StockPt" />
  ///   stores <see cref="StockPt.Date" />, <see cref="StockPt.Close" />,
  ///   <see cref="StockPt.Open" />, <see cref="StockPt.High" />, and
  ///   <see cref="StockPt.Low" /> members.
  ///   For a JapaneseCandleStick chart, the range between opening and closing values
  ///   is drawn as a filled bar, with the filled color different
  ///   (<see cref="ZedGraph.JapaneseCandleStick.RisingFill" />) for the case of
  ///   <see cref="StockPt.Close" />
  ///   higher than <see cref="StockPt.Open" />, and
  ///   <see cref="ZedGraph.JapaneseCandleStick.FallingFill" />
  ///   for the reverse.  The width of the bar is controlled
  ///   by the <see cref="ZedGraph.OHLCBar.Size" /> property, which is specified in
  ///   points (1/72nd inch), and scaled according to <see cref="PaneBase.CalcScaleFactor" />.
  ///   The candlesticks are drawn horizontally or vertically depending on the
  ///   value of <see cref="BarSettings.Base" />, which is a
  ///   <see cref="ZedGraph.BarBase" /> enum type.
  /// </remarks>
  /// <author> John Champion </author>
  /// <version> $Revision: 3.6 $ $Date: 2007-12-31 00:23:05 $ </version>
  [Serializable]
  public class JapaneseCandleStickItem : OHLCBarItem, ICloneable
  {
    #region Fields

    /// <summary>
    ///   Current schema value that defines the version of the serialized file
    /// </summary>
    private const int schema3 = 10;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets a reference to the <see cref="JapaneseCandleStick" /> class defined
    ///   for this <see cref="JapaneseCandleStickItem" />.
    /// </summary>
    public new JapaneseCandleStick Bar => (JapaneseCandleStick)base.Bar;

    #endregion

    #region Constructors

    /// <summary>
    ///   Create a new <see cref="OHLCBarItem" />, specifying only the legend label.
    /// </summary>
    /// <param name="label">The label that will appear in the legend.</param>
    public JapaneseCandleStickItem(string label, int zOrder = -1)
      : base(label, zOrder)
    {}

    /// <summary>
    ///   Create a new <see cref="JapaneseCandleStickItem" /> using the specified properties.
    /// </summary>
    /// <param name="label">The label that will appear in the legend.</param>
    /// <param name="points">
    ///   An <see cref="IPointList" /> of double precision values that define
    ///   the Date, Close, Open, High, and Low values for the curve.  Note that this
    ///   <see cref="IPointList" /> should contain <see cref="StockPt" /> items rather
    ///   than <see cref="PointPair" /> items.
    /// </param>
    public JapaneseCandleStickItem(string label, IPointList points, int zOrder=-1)
      : base(label, points, LineBase.Default.Color, zOrder)
    {}

    /// <summary>
    ///   The Copy Constructor
    /// </summary>
    /// <param name="rhs">The <see cref="JapaneseCandleStickItem" /> object from which to copy</param>
    public JapaneseCandleStickItem(JapaneseCandleStickItem rhs)
      : base(rhs)
    {}

    /// <summary>
    ///   Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
    ///   calling the typed version of <see cref="Clone" />
    /// </summary>
    /// <returns>A deep copy of this object</returns>
    object ICloneable.Clone()
    {
      return Clone();
    }

    /// <summary>
    ///   Typesafe, deep-copy clone method.
    /// </summary>
    /// <returns>A new, independent copy of this class</returns>
    public new JapaneseCandleStickItem Clone()
    {
      return new JapaneseCandleStickItem(this);
    }

    protected override OHLCBar MakeBar(Color color = default(Color))
    {
      return new JapaneseCandleStick();
    }

    #endregion

    #region Serialization

    /// <summary>
    ///   Constructor for deserializing objects
    /// </summary>
    /// <param name="info">
    ///   A <see cref="SerializationInfo" /> instance that defines the serialized data
    /// </param>
    /// <param name="context">
    ///   A <see cref="StreamingContext" /> instance that contains the serialized data
    /// </param>
    protected JapaneseCandleStickItem(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      // The schema value is just a file version parameter.  You can use it to make future versions
      // backwards compatible as new member variables are added to classes
      var sch = info.GetInt32("schema2");
    }

    /// <summary>
    ///   Populates a <see cref="SerializationInfo" /> instance with the data needed to serialize the target object
    /// </summary>
    /// <param name="info">A <see cref="SerializationInfo" /> instance that defines the serialized data</param>
    /// <param name="context">A <see cref="StreamingContext" /> instance that contains the serialized data</param>
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);

      info.AddValue("schema2", schema3);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Draw a legend key entry for this <see cref="OHLCBarItem" /> at the specified location
    /// </summary>
    /// <param name="g">
    ///   A graphic device object to be drawn into.  This is normally e.Graphics from the
    ///   PaintEventArgs argument to the Paint() method.
    /// </param>
    /// <param name="pane">
    ///   A reference to the <see cref="ZedGraph.GraphPane" /> object that is the parent or
    ///   owner of this object.
    /// </param>
    /// <param name="rect">
    ///   The <see cref="RectangleF" /> struct that specifies the
    ///   location for the legend key
    /// </param>
    /// <param name="scaleFactor">
    ///   The scaling factor to be used for rendering objects.  This is calculated and
    ///   passed down by the parent <see cref="ZedGraph.GraphPane" /> object using the
    ///   <see cref="PaneBase.CalcScaleFactor" /> method, and is used to proportionally adjust
    ///   font sizes, etc. according to the actual size of the graph.
    /// </param>
    public override void DrawLegendKey(Graphics g, GraphPane pane, RectangleF rect, float scaleFactor)
    {
      float pixBase, pixHigh, pixLow, pixOpen, pixClose;

      if (pane._barSettings.Base == BarBase.X)
      {
        pixBase = rect.Left + rect.Width/2.0F;
        pixHigh = rect.Top;
        pixLow = rect.Bottom;
        pixOpen = pixHigh + rect.Height/3;
        pixClose = pixLow - rect.Height/3;
      }
      else
      {
        pixBase = rect.Top + rect.Height/2.0F;
        pixHigh = rect.Right;
        pixLow = rect.Left;
        pixOpen = pixHigh - rect.Width/3;
        pixClose = pixLow + rect.Width/3;
      }

      var baseAxis = BaseAxis(pane);
      var halfSize = Bar.GetBarWidth( pane, baseAxis, scaleFactor );
      //var halfSize = Stick.Size * scaleFactor;

      using (var pen = new Pen(Bar.Color, Bar.Width))
      {
        Bar.Draw(g, pane, pane._barSettings.Base == BarBase.X, pixBase, pixHigh,
                    pixLow, pixOpen, pixClose, halfSize, scaleFactor, pen,
                    Bar.RisingFill,
                    Bar.RisingBorder, null, _dotHalfSize * scaleFactor);
      }
    }

    #endregion
  }
}
