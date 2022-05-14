using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace HistoryMap
{
    public partial class MainMap : Form
    {
        public string DynastyPath = string.Empty;
        public MainMap()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            InitializeComponent();
        }
        string dirPath = string.Empty;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //初始化
            //string sPath = System.IO.Path.Combine(Application.StartupPath, @"data\hx\map.mxd");
            //axMapControl1.LoadMxFile(sPath);
            //axMapControl1.Refresh();

            //List<Chaodai> listChaodai = new List<Chaodai>();
            //listChaodai.Add(new Chaodai() { ChaoDaiId = "Now", ChaoDaiName = "当今" });
            //listChaodai.Add(new Chaodai() { ChaoDaiId = "q", ChaoDaiName = "秦" });
            //listChaodai.Add(new Chaodai() { ChaoDaiId = "xh", ChaoDaiName = "西汉" });
            //listChaodai.Add(new Chaodai() { ChaoDaiId = "dh", ChaoDaiName = "东汉" });
            //cbChaodai.DataSource = listChaodai;
            //cbChaodai.DisplayMember = "ChaoDaiName";
            //cbChaodai.ValueMember = "ChaoDaiId";
            //cbChaodai.SelectedIndex = 1;
            dirPath = System.IO.Path.Combine(Application.StartupPath, "data", DynastyPath);
            string[] arrTemp = System.IO.Directory.GetFiles(dirPath, "*.mxd");
            if (arrTemp != null)
            {
                foreach (string s in arrTemp)
                {
                    cbChaodai.Items.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }
            //cbChaodai.Items.Add("当今");
            //cbChaodai.Items.Add("秦");
            //cbChaodai.Items.Add("西汉");
            //cbChaodai.Items.Add("东汉");
            cbChaodai.SelectedIndex = 0;

            axMapControl1.MapUnits = ESRI.ArcGIS.esriSystem.esriUnits.esriMeters;

            this.WindowState = FormWindowState.Maximized;
        }

        //--1、让两个MapControl显示的数据保持一致,让两个控件的数据共享
        //--1、OnMapReplaced当mapcontrol1中的地图被替换时，该方法自动加载主空间中所有的图层对象到鹰眼中
        //--1、而如果使用传递两个控件之间的Map属性这种方法完成数据共享，因为map属性同时传递了地图对象的范围和比例，
        //--1、造成鹰眼时图中不能够显示完整的地图
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            //获得mapcontrol1的map对象
            IMap map = axMapControl1.Map;
            
            //遍历map中的图层，都添加到mapcontrol2中
            for (int i = 0; i < map.LayerCount; i++)
            {
                axMapControl2.AddLayer(map.get_Layer(i));
            }
        }
        //--2、在主控件中使用鼠标拖拽视图的时候，鹰眼控件中出现红色矩形框
        //--2、该方法发生在主视图的范围发生变换后，
        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //得到新范围
            IEnvelope2 envelope = e.newEnvelope as IEnvelope2;
            //生成一个矩形要素
            IRectangleElement rectangleelement = new RectangleElementClass();
            //得到一个要素并将矩形要素传入
            IElement element = rectangleelement as IElement;
            //将新范围赋值给要素的几何外形
            element.Geometry = envelope;
            //得到RGBColor对象
            IRgbColor color = new RgbColorClass();
            //设置Color的RGB值
            color.RGB = 00000255;
            //设置透明度
            color.Transparency = 255;
            //产生一个线状符号
            ILineSymbol lineSymbool = new SimpleLineSymbolClass();
            //设置线状符号的属性
            lineSymbool.Width = 1;
            lineSymbool.Color = color;
            //设置颜色属性
            color.RGB = 255;
            color.Transparency = 0;
            //生成一个填充符号用来填充矩形区域
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            //设置填充符号的属性
            //填充色
            fillSymbol.Color = color;
            //外轮廓
            fillSymbol.Outline = lineSymbool;
            //得到一个填充图形要素，AE帮助中对IFillShapElement的解释为
            /*IFillShapeElement is a generic interface implemented by all 2d elements (CircleElement,
             * EllipseElement, PolygonElement, and RectangleElement).
             * Use this interface when you want to retrieve or set the fill symbol being used by one 
             * of the fill shape elements.
            */
            IFillShapeElement fillShapElement = element as IFillShapeElement;
            //将填充符号传入Symbol中
            fillShapElement.Symbol = fillSymbol;
            //最后将填充图形要素赋值给要素类
            element = fillShapElement as IElement;

            //得到图像容器,并将mapcontrol2的map传入
            IGraphicsContainer graphicsContainer = axMapControl2.Map as IGraphicsContainer;
            //得到一个活动视图并将graphicscontainer传入
            IActiveView activeView = graphicsContainer as IActiveView;

            //在绘制新的矩形框前，清楚Map对象中的任何图形元素
            graphicsContainer.DeleteAllElements();

            //将要素添加到图像容器中,并将其置于顶层0
            graphicsContainer.AddElement(element, 0);
            //将图像容器装入活动视图中并刷新
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);


        }

        private void cbChaodai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbChaodai.Text))
            {
                string Name = cbChaodai.Text;
                axMapControl1.ClearLayers();
                axMapControl2.ClearLayers();

                //加载地图
                string sPath = string.Empty;

                sPath = System.IO.Path.Combine(dirPath, Name + ".mxd");
                //List<string> listSearchType = new List<string>();
                //switch (Name)
                //{
                //    case "当今":
                //        listSearchType.Add("省");
                //        listSearchType.Add("省会");
                //        sPath = System.IO.Path.Combine(Application.StartupPath, @"data\Now\map.mxd");
                //        break;
                //    case "秦":
                //        listSearchType.Add("州");
                //        listSearchType.Add("治所");
                //        listSearchType.Add("其他");
                //        sPath = System.IO.Path.Combine(Application.StartupPath, @"data\q\map.mxd");
                //        break;
                //    case "西汉":
                //        listSearchType.Add("州");
                //        listSearchType.Add("治所");
                //        listSearchType.Add("其他");
                //        sPath = System.IO.Path.Combine(Application.StartupPath, @"data\xh\map.mxd");
                //        break;
                //    case "东汉":
                //        listSearchType.Add("州");
                //        listSearchType.Add("治所");
                //        listSearchType.Add("其他");
                //        sPath = System.IO.Path.Combine(Application.StartupPath, @"data\dh\map.mxd");
                //        break;
                //}
                axMapControl1.LoadMxFile(sPath);
                axMapControl1.Refresh();


                //listSearchType.Add(new SearchType() { SearchCode = "historyevent", SearchName = "历史事件" });
                //cbSearchType.DataSource = listSearchType;
                cbSearchType.Items.Clear();
                for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
                {
                    ILayer layer =axMapControl1.Map.get_Layer(i);
                    cbSearchType.Items.Add(layer.Name);
                }
                //foreach(ILayer layer in axMapControl1.layer)
                //foreach (string s in listSearchType)
                //{
                //    cbSearchType.Items.Add(s);
                //}
                cbSearchType.SelectedIndex = 0;
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbSearchType.Text.Trim()))
            {
                MessageBox.Show("请选择查询类型！");
                cbSearchType.Focus();
                return;
            }
            if (String.IsNullOrEmpty(tbKeyWords.Text.Trim()))
            {
                MessageBox.Show("请输入查询关键字！");
                tbKeyWords.Focus();
                return;
            }
            ILayer SearchLayer = GetLayer(cbSearchType.Text); ;
            //switch (cbSearchType.Text)
            //{
            //    case "省":
            //        SearchLayer = GetLayer("sheng");
            //        break;
            //    case "省会":
            //        SearchLayer = GetLayer("shenghui");
            //        break;
            //    case "州":
            //        SearchLayer = GetLayer("zhou");
            //        break;
            //    case "治所":
            //        SearchLayer = GetLayer("zhisuo");
            //        break;
            //    case "其他":
            //        SearchLayer = GetLayer("qita");
            //        break;
            //    //case "历史事件":
            //    //    SearchLayer = GetLayer("");
            //    //    break;
            //    default:
            //        MessageBox.Show("请选择查询类型！");
            //        cbSearchType.Focus();
            //        return;
            //        break;
            //}
            if (SearchLayer != null)
            {
                SearchFeature(SearchLayer, tbKeyWords.Text);
            }
            else
            {
                MessageBox.Show("图层不存在！");
            }
        }

        private ILayer GetLayer(string LayerName)
        {
            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                ILayer pLayer = axMapControl1.Map.get_Layer(i);
                if (pLayer.Name.Equals(LayerName))
                {
                    return pLayer;
                }
            }
            return null;

        }

        public void SearchFeature(ILayer layer, string KeyWords)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            //获取featureLayer的featureClass 
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IFeature feature = null;
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureCursor featureCusor;
            queryFilter.WhereClause = "1=1";
            featureCusor = featureClass.Search(queryFilter, true);
            //查找出所有点并计算距离
            while ((feature = featureCusor.NextFeature()) != null)
            {
                bool bContain = false;
                string temp = string.Empty;
                for (int i = 0; i < feature.Fields.FieldCount; i++)
                {
                    if (feature.get_Value(i) != null && feature.get_Value(i).ToString().Contains(KeyWords))
                    {
                        for (int j = 0; j < feature.Fields.FieldCount; j++)
                        {
                            if (!"FID".Equals(feature.Fields.get_Field(j).AliasName.ToUpper())
                                && !"SHAPE".Equals(feature.Fields.get_Field(j).AliasName.ToUpper())
                                && !"ID".Equals(feature.Fields.get_Field(j).AliasName.ToUpper()))
                            temp += feature.Fields.get_Field(j).AliasName + ":" + feature.get_Value(j)+"\r\n";
                        }
                        bContain = true;
                        break;
                    }
                }
                if (bContain)
                {
                    axMapControl1.ActiveView.Extent = feature.Shape.Envelope;
                    axMapControl1.Refresh();
                    axMapControl1.FlashShape(feature.Shape);
                    MessageBox.Show(temp);
                }
            }
        }

        public void GetKeyWords(ILayer layer)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            //获取featureLayer的featureClass 
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IFeature feature = null;
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureCursor featureCusor;
            queryFilter.WhereClause = "1=1";
            featureCusor = featureClass.Search(queryFilter, true);
            //查找出所有点并计算距离
            int NameIndex = -1;
            while ((feature = featureCusor.NextFeature()) != null)
            {
                if (NameIndex < 0)
                {
                    NameIndex = feature.Fields.FindField("Name");
                }
                tbKeyWords.Items.Add(feature.get_Value(NameIndex));
            }
        }

        private void btInfo_Click(object sender, EventArgs e)
        {
            InfoWindow frmInfoWindow = new InfoWindow(cbChaodai.Text);
            frmInfoWindow.MultiMediaPath = System.IO.Path.Combine(dirPath, cbChaodai.Text + "多媒体资料");
            frmInfoWindow.ShowDialog();
        }

        private void axMapControl1_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnDoubleClickEvent e)
        {
            IPoint point = new PointClass();
            point.X = e.mapX;
            point.Y = e.mapY;
            double distance = 10;
            string temp = string.Empty;

            ILayer ZhouLayer = GetLayer("zhou");
            if (ZhouLayer != null)
            {
                temp += "州:" + GetNearPoint(point, ZhouLayer, distance, "Name") + "\r\n";
            }

            ILayer ZhisuoLayer = GetLayer("zhisuo");
            if (ZhisuoLayer != null)
            {
                temp += "治所:" + GetNearPoint(point, ZhisuoLayer, distance, "Name") + "\r\n";
            }


            ILayer ShengLayer = GetLayer("sheng");
            if (ShengLayer != null)
            {
                temp += "省:" + GetNearPoint(point, ShengLayer, distance, "Name") + "\r\n";
            }

            ILayer ShenghuiLayer = GetLayer("shenghui");
            if (ShenghuiLayer != null)
            {
                temp += "省会:" + GetNearPoint(point, ShenghuiLayer, distance, "Name") + "\r\n";
            }

            ILayer QitaLayer = GetLayer("qita");
            if (QitaLayer != null)
            {
                temp += "其他:" + GetNearPoint(point, QitaLayer, distance, "Name") + "\r\n";
            }
            MessageBox.Show(temp);
        }

        public string GetNearPoint(IPoint point, ILayer layer, double distance, string FieldName)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            //获取featureLayer的featureClass 
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IFeature feature = null;
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureCursor featureCusor;
            queryFilter.WhereClause = "1=1";
            featureCusor = featureClass.Search(queryFilter, true);
            double angle = 0;
            string nearName = string.Empty;

            string temp = string.Empty;
            int findex = -1;
            //查找出所有点并计算距离
            while ((feature = featureCusor.NextFeature()) != null)
            {
                IPoint point2 = null;
                if (feature.Shape is IPoint)
                {
                    point2 = (feature.Shape as IPoint);
                }
                else if (feature.Shape is IArea)
                {
                    point2 = (feature.Shape as IArea).Centroid;
                }

                if (point2 != null)
                {

                    double tempLength = getDistanceOfTwoPoints(point2.X, point2.Y, point.X, point.Y);
                    if (tempLength < distance)
                    {
                        axMapControl1.FlashShape(feature.Shape);
                        //在范围内
                        if (findex == -1)
                        {
                            findex = feature.Fields.FindField(FieldName);
                        }
                        temp += feature.get_Value(findex) + ",";
                    }
                }
            }
            return temp.TrimEnd(',');
        }

        public double getDistanceOfTwoPoints(double X1, double Y1, double X2, double Y2)
        {
            return Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2));
            //return ConvertPixelsToMapUnits(axMapControl1.ActiveView,Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2)));
        }

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbKeyWords.Items.Clear();
            ILayer SearchLayer = null;
            SearchLayer = GetLayer(cbSearchType.Text);
            //switch (cbSearchType.Text)
            //{
            //    case "省":
            //        SearchLayer = GetLayer("sheng");
            //        break;
            //    case "省会":
            //        SearchLayer = GetLayer("shenghui");
            //        break;
            //    case "州":
            //        SearchLayer = GetLayer("zhou");
            //        break;
            //    case "治所":
            //        SearchLayer = GetLayer("zhisuo");
            //        break;
            //    case "其他":
            //        SearchLayer = GetLayer("qita");
            //        break;
            //    default:
            //        return;
            //        break;
            //}
            if (SearchLayer != null)
            {
                GetKeyWords(SearchLayer);
            }
        }

        private void MainMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
    }



    public class Chaodai
    {
        /// <summary>
        /// 朝代代号 与地图文件夹关联
        /// </summary>
        public string ChaoDaiId
        {
            get;
            set;
        }
        /// <summary>
        /// 朝代名称
        /// </summary>
        public string ChaoDaiName
        {
            get;
            set;
        }
    }

    public class SearchType
    {
        /// <summary>
        /// 查找CODE
        /// </summary>
        public string SearchCode
        {
            get;
            set;
        }
        /// <summary>
        /// 查找名称
        /// </summary>
        public string SearchName
        {
            get;
            set;
        }
    }
}
