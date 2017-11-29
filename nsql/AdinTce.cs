﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;

using IQueryManagers;

using QueryManagers;

namespace AdinTce
{

    #region AdinTce

    //adin tce repository
    public class AdinTceRepo
    {

        IQueryManagers.ICommandBuilder _CommandBuilder;
        IWebManagers.IWebManager _webManager;
        IWebManagers.IResponseReader _responseReader;
        IJsonManagers.IJsonManger _jsonManager;     

        IQueryManagers.ITypeToken GUIDtoken;

        AdinTceExplicitTokenBuilder tokenBuilder;

        public AdinTceRepo(
            IQueryManagers.ICommandBuilder CommandBuilder_,
            IWebManagers.IWebManager webManager_,
            IWebManagers.IResponseReader responseReader_,
            IJsonManagers.IJsonManger jsonManager_)
        {
            this._CommandBuilder = CommandBuilder_;
            this._webManager = webManager_;
            this._responseReader = responseReader_;
            this._jsonManager = jsonManager_;

            GUIDtoken = new AdinTceGUIDToken();
            tokenBuilder = new AdinTceExplicitTokenBuilder();
        }

        public AdinTceRepo()
        {

            this._CommandBuilder = new AdinTceCommandBuilder(new TokenMiniFactory(), new FormatFactory());
            this._webManager = new AdinTceWebManager();
            this._responseReader = new AdinTceResponseReader();
            this._jsonManager = new AdinTceJsonManager();

            _webManager.AddCredentials(new System.Net.NetworkCredential(
               ConfigurationManager.AppSettings["AdinTceLogin"], ConfigurationManager.AppSettings["AdinTcePassword"]));

            GUIDtoken = new AdinTceGUIDToken();
            tokenBuilder = new AdinTceExplicitTokenBuilder();
        }
        public string HoliVation(string GUID)
        {
            
            AdinTcePOCO adp = new AdinTcePOCO();
            IEnumerable<Holiday> dhl=null;
            IEnumerable<Vacation> vhl = null;
            IEnumerable<GraphRead> ghl = null;
            IEnumerable<GUIDPOCO> gpl = null;

            string result = string.Empty;
            string holidayCommand, vacationCommand, graphCommand,
                holidaysResp = string.Empty, vacationsResp = string.Empty, graphResp = string.Empty;
            GUIDtoken.Text = GUID;

            _CommandBuilder.SetText(tokenBuilder.HolidaysCommand(GUIDtoken), new AdinTcePartformat());
            holidayCommand = _CommandBuilder.GetText();
            _CommandBuilder.SetText(tokenBuilder.VacationsCommand(GUIDtoken), new AdinTcePartformat());
            vacationCommand = _CommandBuilder.GetText();
            _CommandBuilder.SetText(tokenBuilder.GraphCommand(GUIDtoken), new AdinTcePartformat());
            graphCommand = _CommandBuilder.GetText();


            Task[] tasks = new Task[3];

            _webManager.AddRequest(holidayCommand);
            tasks[0]=Task.Factory.StartNew(async () => holidaysResp = _responseReader.ReadResponse(_webManager.GetResponse("GET")));
            _webManager.AddRequest(vacationCommand);
            tasks[1]=Task.Factory.StartNew(async () => vacationsResp = _responseReader.ReadResponse(_webManager.GetResponse("GET")));
            _webManager.AddRequest(graphCommand);
            tasks[2]=Task.Factory.StartNew(async () => graphResp = _responseReader.ReadResponse(_webManager.GetResponse("GET")));

            Task.WaitAll(tasks);

			if (holidaysResp != null && holidaysResp != string.Empty)
            {
                if (adp == null)
                {
                    adp = new AdinTcePOCO();                
                    gpl = _jsonManager.DeserializeFromParentNode<GUIDPOCO>(holidaysResp);
                    dhl = _jsonManager.DeserializeFromParentChildren<Holiday>(holidaysResp, "Holidays");
                }
                adp.GUID_ = gpl.Select(s => s).FirstOrDefault().GUID_;
                adp.Position = gpl.Select(s => s).FirstOrDefault().Position;
                adp.holidays = dhl.ToList();
            }


          
            if (vacationsResp!=null && vacationsResp != string.Empty)
            {
                if (adp == null)
                {
                    adp = new AdinTcePOCO();
                    gpl = _jsonManager.DeserializeFromParentNode<GUIDPOCO>(holidaysResp);
                    dhl = _jsonManager.DeserializeFromParentChildren<Holiday>(holidaysResp, "Holidays");
                }
                vhl = _jsonManager.DeserializeFromParentChildren<Vacation>(vacationsResp, "Holidays");
                adp.vacations = vhl.ToList();
            }
          
            if (graphResp != null && graphResp != string.Empty)
            {
                if (adp == null)
                {
                    adp = new AdinTcePOCO();
                    gpl = _jsonManager.DeserializeFromParentNode<GUIDPOCO>(holidaysResp);
                    dhl = _jsonManager.DeserializeFromParentChildren<Holiday>(holidaysResp, "Holidays");
                }
                ghl = _jsonManager.DeserializeFromParentChildren<GraphRead>(graphResp, "Holidays");
                adp.Graphs = GrapthReadToWriteDateCheck(ghl.ToList());
            }

			
            //Task.Run(() => {
            _webManager.AddRequest(holidayCommand);
            holidaysResp = _responseReader.ReadResponse(_webManager.GetResponse("GET"));
            if (holidaysResp != null && holidaysResp != string.Empty)
            {
                gpl = _jsonManager.DeserializeFromParentNode<GUIDPOCO>(holidaysResp);
                dhl = _jsonManager.DeserializeFromParentChildren<Holiday>(holidaysResp, "Holidays");
                adp.GUID_ = gpl.Select(s => s).FirstOrDefault().GUID_;
                adp.Position = gpl.Select(s => s).FirstOrDefault().Position;
                adp.holidays = dhl.ToList();
            }
            _webManager.AddRequest(holidayCommand);
            vacationsResp = _responseReader.ReadResponse(_webManager.GetResponse( "GET"));
            if (vacationsResp!=null && vacationsResp != string.Empty)
            {
                vhl = _jsonManager.DeserializeFromParentChildren<Vacation>(vacationsResp, "Holidays");
                adp.vacations = vhl.ToList();
            }

            //});

            result = _jsonManager.SerializeObject(adp);

            return result;
        }


        private async Task<string> Request(string command_)
        {
            _webManager.AddRequest(command_);
            Task<string> task_ = Task.Run(
                    () =>                         
                        _responseReader.ReadResponse(_webManager.GetResponse("GET"))                    
                );
            await task_;
            return task_.Result;
        }
      
  public List<GraphWrite> GrapthReadToWriteDateCheck(List<GraphRead> ghl_)
        {
            List<GraphWrite> gw = new List<GraphWrite>();
            foreach (GraphRead gr in ghl_)
            {
                DateTime? a;
                GraphWrite gfw = new GraphWrite();
                if (gr.DateFinish == new DateTime()) { gfw.DateFinish = null; } else { gfw.DateFinish = gr.DateFinish; }
                if (gr.DateStart == new DateTime()) { gfw.DateStart = null; } else { gfw.DateStart = gr.DateStart; }
                gfw.LeaveType = gr.LeaveType;
                gfw.DaysSpent = gr.DaysSpent;

                gw.Add(gfw);

            }
            return gw;
        }
    }

    //Command buil from Tokens,with explicit sytax for repo call
    public class AdinTceExplicitTokenBuilder
    {

        public List<IQueryManagers.ITypeToken> HolidaysCommand(IQueryManagers.ITypeToken GUID)
        {
            List<IQueryManagers.ITypeToken> result = new List<IQueryManagers.ITypeToken>()
            { new AdinTceURLToken(), new AdinTceHolidatyToken(), new AdinTcePartToken(), GUID};
            return result;
        }
        public List<IQueryManagers.ITypeToken> VacationsCommand(IQueryManagers.ITypeToken GUID)
        {
            List<IQueryManagers.ITypeToken> result = new List<IQueryManagers.ITypeToken>()
            { new AdinTceURLToken(), new AdinTceVacationToken(), new AdinTcePartToken(), GUID};
            return result;
        }

  public List<IQueryManagers.ITypeToken>GraphCommand(IQueryManagers.ITypeToken GUID)
        {
            List<IQueryManagers.ITypeToken> result = new List<IQueryManagers.ITypeToken>()
            { new AdinTceURLToken(), new AdinTceGraphToken(), new AdinTcePartToken(), GUID};
            return result;
        }
    }


    //AdinTce Tokens
    public class AdinTceURLToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = ConfigurationManager.AppSettings["AdinTceUrl"];
    }
 public class AdinTceGraphToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"graph";
    }
    public class AdinTceHolidatyToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"holiday";
    }
    public class AdinTceVacationToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"vacation";
    }
    public class AdinTceFullToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"full";
    }
    public class AdinTcePartToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"part";
    }
    public class AdinTceGUIDToken : IQueryManagers.ITypeToken
    {
        public string Text { get; set; }
    }

    //AdinTce formats
    public class AdinTceURLformat : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"{0}/{1}/{2}";
    }
    public class AdinTcePartformat : IQueryManagers.ITypeToken
    {
        public string Text { get; set; } = @"{0}/{1}/{2}/{3}";
    }



    ///<summary>AdinTce realization of Base builder,web,reader,json
    ///</summary>
    public class AdinTceCommandBuilder : QueryManagers.CommandBuilder
    {
        public AdinTceCommandBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatFactory_) 
            : base(tokenFactory_, formatFactory_)
        {
            
        }
    }

    public class AdinTceWebManager : WebManagers.WebManager
    {

    }

    public class AdinTceResponseReader : WebManagers.WebResponseReader
    {

    }

    public class AdinTceJsonManager : JsonManagers.JSONManager
    {

    }

    public class AdinTceCommands
    {

    }



    //AdinTce POCOs
    class MonthDayYearDateConverter : IsoDateTimeConverter
    {
        public MonthDayYearDateConverter()
        {
            DateTimeFormat = "dd.MM.yyyy";
        }
    }

	class MonthDayYearDateNoDotsConverter : IsoDateTimeConverter
    {
        public MonthDayYearDateNoDotsConverter()
        {
            DateTimeFormat = "yyyyMMdd";
        }
    }
    public class Holiday
    {
		public Holiday()
        {        
            LeaveType = null;
            Days = 0;
        }
        [JsonProperty("LeaveType")]
        public string LeaveType { get; set; }
        [JsonProperty("Days")]
        public double Days { get; set; }
    }
   
    public class Vacation
    {

		public Vacation()
        {
            DateStart = null;
            DateFinish = null;
            LeaveType = null;
            DaysSpent = 0;
        }
        [JsonProperty("LeaveType")]
        public string LeaveType { get; set; }
        [JsonProperty("DateStart"), JsonConverter(typeof(MonthDayYearDateConverter))]
        public DateTime? DateStart { get; set; }
        [JsonProperty("DateFinish"), JsonConverter(typeof(MonthDayYearDateConverter))]
        public DateTime? DateFinish { get; set; }
        [JsonProperty("DaysSpent")]
        public int DaysSpent { get; set; }
    }
   

    public class GraphRead
    {
        public GraphRead()
        {
            DateStart = null;
            DateFinish = null;
            LeaveType = null;
            DaysSpent = 0;
        }
        [JsonProperty("LeaveType")]
        public string LeaveType { get; set; }
        [JsonProperty("DateStart"), JsonConverter(typeof(MonthDayYearDateNoDotsConverter))]
        public DateTime? DateStart { get; set; }
        [JsonProperty("DateFinish"), JsonConverter(typeof(MonthDayYearDateNoDotsConverter))]
        public DateTime? DateFinish { get; set; }
        [JsonProperty("Days")]
        public int DaysSpent { get; set; }
    }
    public class GraphWrite : GraphRead
    {
        public GraphWrite()
        {
            DateStart = null;
            DateFinish = null;
            LeaveType = null;
            DaysSpent = 0;
        }
        [JsonProperty("DateStart"), JsonConverter(typeof(MonthDayYearDateConverter))]
        new public DateTime? DateStart { get; set; }
        [JsonProperty("DateFinish"), JsonConverter(typeof(MonthDayYearDateConverter))]
        new public DateTime? DateFinish { get; set; }
       
    }
    public class GUIDPOCO
    {
        [JsonProperty("GUID")]
        public string GUID_ { get; set; }
        [JsonProperty("Position")]
        public string Position { get; set; }
    }
  
    public class AdinTcePOCO
    {
        [JsonProperty("GUID")]
        public string GUID_ { get; set; }
        [JsonProperty("Position")]
        public string Position { get; set; }
        [JsonProperty("Holidays")]
        public List<Holiday> holidays { get; set; }
        [JsonProperty("Vacations")]
        public List<Vacation> vacations { get; set; }
        [JsonProperty("Graphs")]
        public List<GraphWrite> Graphs { get; set; }
    }
    #endregion

}