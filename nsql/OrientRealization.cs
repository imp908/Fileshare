﻿
using System;
using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Configuration;

using WebManagers;
using IQueryManagers;
using QueryManagers;
using IOrientObjects;

using POCO;


/// <summary>
/// Realization of IJsonMangers, IWebManagers, and IOrient specifically for orient db
/// </summary>
namespace OrientRealization
{

    /// <summary>
    ///     Orient specific WebManager for authentication and authenticated responses sending to URL
    ///     with NetworkCredentials
    /// </summary>    

    public class OrientWebManager : WebManager
    {
        string OSESSIONID=string.Empty;

        //>> add async
        public new HttpWebResponse GetResponse(string url, string method)
        {

            //HttpWebResponse resp=null;
            base.AddRequest(url, method);
            base.AddHeader(HttpRequestHeader.Cookie, this.OSESSIONID);

            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            return null;
        }
        public HttpWebResponse GetResponseCred(string method_)
        {

            //HttpWebResponse resp=null; base.addRequest(url, method);
            this._request.Method=method_;
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            return null;
        }
        public WebResponse Authenticate(string url, NetworkCredential nc=null)
        {

            WebResponse resp;
            AddRequest(url, "GET");
            if (nc==null) {CredentialsBind();}
            else {SetCredentials(nc);}
            try
            {
                resp=this._request.GetResponse();
                OSESSIONID=GetHeaderValue("Set-Cookie");
                return resp;
            }
            catch (Exception e)
            {
                throw e;
           }

        }

    }

    #region Tokens

    /// <summary>
    ///  Tokens realization for different string concatenations
    /// </summary>

    //Tokens for Orient Comamnd and Authenticate URLs
    public class OrientHost : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["ParentHost"];
   }
    public class OrientDatabaseNameToken : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["ParentDBname"];
   }
    public class OrientPort : ITypeToken
    {
        public string Text {get; set;}="2480";
   }
    public class OrientDatabaseCRUDToken : ITypeToken
    {
        public string Text {get; set;}="Connect";
   }
    public class OrientAuthenticateToken : ITypeToken
    {
        public string Text {get; set;}="connect";
   }
    public class OrientCommandToken : ITypeToken
    {
        public string Text {get; set;}="command";
   }
    public class OrientCommandSQLTypeToken : ITypeToken
    {
        public string Text {get; set;}="sql";
   }
    public class OrientFuncionToken : ITypeToken
    {
        public string Text {get; set;}="function";
   }
    public class OrientBatchToken : ITypeToken
    {
        public string Text {get; set;}="batch";
   }

    /// <summary>
    /// Orient query tokens
    /// </summary>  

    //Orient SQL syntax tokens
    public class OrientURLDatabaseToken : ITypeToken
    {
        public string Text {get; set;}="database";
   }
    public class OrientPlocalToken : ITypeToken
    {
        public string Text {get; set;}="plocal";
   }
    public class OrientExpandToken : ITypeToken
    {
        public string Text {get; set;}="expand";
   }
    public class OrientContentToken : ITypeToken
    {
        public string Text {get; set;}="content";
   }
    public class OrientEdgeToken : ITypeToken
    {
        public string Text {get; set;}="Edge";
   }
    public class OrientPropertyToken : ITypeToken
    {
        public string Text {get; set;}="Property";
   }
    public class OrientToToken : ITypeToken
    {
        public string Text {get; set;}="To";
   }
    public class OrientInToken : ITypeToken
    {
        public string Text {get; set;}="in";
   }
    public class OrientOutToken : ITypeToken
    {
        public string Text {get; set;}="out";
   }
    public class OrientEToken : ITypeToken
    {
        public string Text {get; set;}="E";
   }
    public class OrientVToken : ITypeToken
    {
        public string Text {get; set;}="V";
   }
    public class OrientVertexToken : ITypeToken
    {
        public string Text {get; set;}="Vertex";
   }
    public class OrientExtendsToken : ITypeToken
    {
        public string Text {get; set;}="Extends";
   }
    public class OrientMandatoryToken : ITypeToken
    {
        public string Text {get; set;}="MANDATORY";
   }
    public class OrientNotNULLToken : ITypeToken
    {
        public string Text {get; set;}="NOTNULL";
   }
    //Orient URL command and batch body tokens 
    public class OrientBodyConnectToken : ITypeToken
    {
        public string Text {get; set;}="connect";
   }
    public class OrientBodyCommandToken : ITypeToken
    {
        public string Text {get; set;}="command";
   }
    public class OrientBodyTransactionToken : ITypeToken
    {
        public string Text {get; set;}="transaction";
   }
    public class OrientBodyOperationToken : ITypeToken
    {
        public string Text {get; set;}="operations";
   }
    public class OrientBodyTypeToken : ITypeToken
    {
        public string Text {get; set;}="type";
   }
    public class OrientBodyLanguageToken : ITypeToken
    {
        public string Text {get; set;}="language";
   }
    public class OrientBodyScriptToken : ITypeToken
    {
        public string Text {get; set;}="script";
   }


    //SQL tokens
    public class OrientSelectToken : ITypeToken
    {
        public string Text {get; set;}="Select";
   }
    public class OrientFromToken : ITypeToken
    {
        public string Text {get; set;}="from";
   }
    public class OrientWhereToken : ITypeToken
    {
        public string Text {get; set;}="where";
   }
    public class OrientCreateToken : ITypeToken
    {
        public string Text {get; set;}="Create";
   }
    public class OrientDeleteToken : ITypeToken
    {
        public string Text {get; set;}="Delete";
   }
    public class OrientDropToken : ITypeToken
    {
        public string Text {get; set;}="Drop";
   }
    
    //Overall command tokens
    public class OrientTypeToken : ITypeToken
    {
        public string Text {get; set;}="Type";
   }
    public class OrientAndToken : ITypeToken
    {
        public string Text {get; set;}=@"and";
   }
    public class OrientClassToken : ITypeToken
    {
        public string Text {get; set;}="Class";
   }
    public class OrientEqualsToken : ITypeToken
    {
        public string Text {get; set;}=@"=";
   }
    public class OrientApostropheToken : ITypeToken
    {
        public string Text {get; set;}=@"'";
   }
    public class OrientDoubleQuotesToken : ITypeToken
    {
        public string Text {get; set;}=@"""";
   }
    public class OrientGapToken : ITypeToken
    {
        public string Text {get; set;}=@" ";
   }
    public class OrientDotToken : ITypeToken
    {
        public string Text {get; set;}=@".";
   }
    public class OrientCommaToken : ITypeToken
    {
        public string Text {get; set;}=@",";
   }
    public class OrientRoundBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"(";
   }
    public class OrientRoundBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@")";
   }
    public class OrientFigureBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"{";
   }
    public class OrientFigureBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@"}";
   }
    public class OrientSquareBraketLeftToken : ITypeToken
    {
        public string Text {get; set;}=@"[";
   }
    public class OrientSquareBraketRightToken : ITypeToken
    {
        public string Text {get; set;}=@"]";
   }
    public class OrientTRUEToken : ITypeToken
    {
        public string Text {get; set;}=@"TRUE";
   }
    public class OrientFLASEToken : ITypeToken
    {
        public string Text {get; set;}=@"FALSE";
   }

    //URI tokens
    public class ColonToken : ITypeToken
    {
        public string Text {get; set;}=":";
   }
    public class SlashToken : ITypeToken
    {
        public string Text {get; set;}="\\";
   }
    public class BackSlashToken : ITypeToken
    {
        public string Text {get; set;}="/";
   }
   

    public class OrientIdSharpToken : ITypeToken
    {
        public string Text {get; set;}=@"#";
   }
    public class OrientIdToken : ITypeToken
    {
        public string Text {get; set;}="Id";
   }
  

    //orient dateformats tokens
    public class OrientSTRINGToken : ITypeToken
    {
        public string Text {get; set;}="STRING";
   }
    public class OrientDATEToken : ITypeToken
    {
        public string Text {get; set;}="DATE";
   }
    public class OrientINTEGERToken : ITypeToken
    {
        public string Text {get; set;}="INTEGER";
   }
    public class OrientDATETIMEToken : ITypeToken
    {
        public string Text {get; set;}="DATETIME";
   }

        
    //orient Class tokens
    public class OrientPersonToken : ITypeToken
    {
        public string Text {get; set;}="Person";
   }
    public class OrientUnitToken : ITypeToken
    {
        public string Text {get; set;}="Unit";
   }
    public class OrientSubUnitToken : ITypeToken
    {
        public string Text {get; set;}="SubUnit";
   }
    public class OrientMainAssignmentToken : ITypeToken
    {
        public string Text {get; set;}="MainAssignment";
   }
    public class OrientTrackBirthdaysToken : ITypeToken
    {
        public string Text {get; set;}="TrackBirthdays";
   }
    public class OrientNameToken : ITypeToken
    {
        public string Text {get; set;}="Name";
   }
    public class OrientAccountToken : ITypeToken
    {
        public string Text {get; set;}="sAMAccountName";
   }
    public class OrientBrewery : ITypeToken
    {
        public string Text {get; set;}="Brewery";
   }
    public class OrientUserSettingsToken : ITypeToken
    {
        public string Text {get; set;}="UserSettings";
   }
    public class OrientCommonSettingsToken : ITypeToken
    {
        public string Text {get; set;}="CommonSettings";
   }
    public class OrientTestDbToken : ITypeToken
    {
        public string Text {get; set;}=ConfigurationManager.AppSettings["TestDBname"];
   }

    ///<summary>
    ///Web response reader Tokens
    /// </summary>

    public class RESULT : ITypeToken
    {
        public string Text {get; set;}="result";
   }
    #endregion

    #region Formats
    public class PropertyItemFormat : ITypeToken
    {
        public string Text {get;set;}=@"{0} {1}{2}{3}";
   }
    #endregion

    #region Factories

    public class OrientQueryFactory : TokenMiniFactory, IOrientQueryFactory
    {

        public ITypeToken CreateToken()
        {
            return new OrientCreateToken();
       }

        public ITypeToken PropertyItemFormatToken()
        {
            return new PropertyItemFormat();
       }

        public ITypeToken SelectToken()
        {
            return new OrientSelectToken ();
       }
        public ITypeToken FromToken()
        {
            return new OrientFromToken();
       }
        public ITypeToken ToToken()
        {
            return new OrientToToken();
       }

        public ITypeToken WhereToken()
        {
            return new OrientWhereToken();
       }

        public ITypeToken ClassToken()
        {
            return new OrientClassToken();
       }
        public ITypeToken ExtendsToken()
        {
            return new OrientExtendsToken();
       }
        public ITypeToken ContentToken()
        {
            return new OrientContentToken();
       }

        public ITypeToken VertexToken()
        {
            return new OrientVertexToken();
       }
        public ITypeToken EdgeToken()
        {
            return new OrientEdgeToken();
       }

        public ITypeToken PropertyToken()
        {
            return new OrientPropertyToken();
       }
        public ITypeToken PropertyTypeToken()
        {
            return new OrientTypeToken();
       }

        public ITypeToken LeftRoundBraket()
        {
            return new OrientRoundBraketLeftToken();
       }
        public ITypeToken RightRoundBraket()
        {
            return new OrientRoundBraketRightToken();
       }
        public ITypeToken LeftSquareBraket()
        {
            return new OrientSquareBraketLeftToken();
       }
        public ITypeToken RightSquareBraket()
        {
            return new OrientSquareBraketRightToken();
       }

        public ITypeToken Equals()
        {
            return new OrientEqualsToken();
       }

        public ITypeToken Mandatory()
        {
            return new OrientMandatoryToken();
       }
        public ITypeToken NotNull()
        {
            return new OrientNotNULLToken();
       }

   }
    public class OrientBodyFactory : IOrientBodyFactory
    {

        public ITypeToken PLocal()
        {
            return new OrientPlocalToken();
       }
        public ITypeToken Connect()
        {
            return new OrientBodyConnectToken();
       }
        public ITypeToken Comma()
        {
            return new OrientCommaToken();
       }
        public ITypeToken True()
        {
            return new OrientTRUEToken();
       }
        public ITypeToken Content()
        {
            return new OrientContentToken();
       }

        // \
        public ITypeToken BackSlash()
        {
            return new BackSlashToken();
       }

        public ITypeToken LeftSqGap()
        {
            return new OrientSquareBraketLeftToken();
       }
        public ITypeToken RightSqGap()
        {
            return new OrientSquareBraketRightToken();
       }

        //:
        public ITypeToken Colon()
        {
            return new ColonToken();
       }      
        // {
        public ITypeToken LeftFgGap()
        {
            return new OrientFigureBraketLeftToken();
       }
        //}
        public ITypeToken RightFgGap()
        {
            return new OrientFigureBraketRightToken();
       }
        // "
        public ITypeToken Quotes()
        {
            return new OrientDoubleQuotesToken();
       }

        public ITypeToken Batch()
        {
            return new OrientBatchToken();
       }

        public ITypeToken Database()
        {
            return new OrientURLDatabaseToken();
       }

        public ITypeToken Command()
        {
            return new OrientBodyCommandToken();
       }
        public ITypeToken Transactions()
        {
            return new OrientBodyTransactionToken();
       }
        public ITypeToken Operations()
        {
            return new OrientBodyOperationToken();
       }
        public ITypeToken Type()
        {
            return new OrientBodyTypeToken();
       }

        public ITypeToken Language()
        {
            return new OrientBodyLanguageToken();
       }
        public ITypeToken sql()
        {
            return new OrientCommandSQLTypeToken();
       }
        public ITypeToken Sctipt()
        {
            return new OrientBodyScriptToken();
       }
   }    

    #endregion



    /// <summary>
    /// Chains cmmand with parameters.
    /// </summary>
    public class CommandsChain
    {
        //contains basic syntax factory
        ITokenMiniFactory _tokenMiniFactory;
        //contains specific orient syntax factory
        IOrientQueryFactory _tokenOrientFactory;
        //format generators factory
        IFormatFactory _formatFactory;
        //commandBuilder factory
        ICommandFactory _commandFactory;

        //commandbuilder for chaining        
        ICommandBuilder _commandBuilder;

        List<ICommandBuilder> _commands;
        IFormatFromListGenerator _formatGenerator;
        
        CommandShemasExplicit _commandShemas;

        public CommandsChain(
            ITokenMiniFactory tokenMiniFactory_,
            IOrientQueryFactory tokenQueryFactory_,
            IFormatFactory formatFactory_,
            ICommandFactory commandFactory_)
        {
            
            this._tokenMiniFactory=tokenMiniFactory_;
            this._tokenOrientFactory=tokenQueryFactory_;
            this._formatFactory=formatFactory_;
            this._commandFactory=commandFactory_;

            this._commandBuilder=this._commandFactory.CommandBuilder(this._tokenMiniFactory, this._formatFactory);
            this._formatGenerator=_formatFactory.FormatGenerator(this._tokenMiniFactory);

            _commands=new List<ICommandBuilder>();
            
            _commandShemas=
                new CommandShemasExplicit(
                    this._commandFactory
                    ,this._formatFactory
                    ,this._tokenMiniFactory
                    ,this._tokenOrientFactory
                    );
        }

        public IFormatFromListGenerator GetGenerator()
        {
            return this._formatGenerator;
        }
        public string GetCommand()
        {
            if(this._commandBuilder.Text==null)
            {
                this._commandBuilder.Build();
            }
            return this._commandBuilder.Text.Text;
        }
        public ICommandBuilder GetBuilder()
        {
            return this._commandBuilder;
        }
              
        

        public CommandsChain NestSq()
        {
            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, _tokenOrientFactory.LeftSquareBraket(), _tokenOrientFactory.RightSquareBraket());
            return this;
        }
        public CommandsChain NestRnd()
        {

            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, _tokenOrientFactory.LeftRoundBraket(), _tokenOrientFactory.RightRoundBraket());
            return this;
        }
        public CommandsChain Nest(ITypeToken leftToken, ITypeToken rightToken)
        {
            this._commandBuilder=_commandShemas.Nest(this._commandBuilder, leftToken, rightToken);
            return this;
        }
        public CommandsChain Nest(ITypeToken leftToken_=null, ITypeToken rightToken_=null, ITypeToken format=null)
        {
            this._commands.Clear();
            this._commands.Add(this._commandShemas.Nest(this._commandBuilder, leftToken_, rightToken_, format));
            this._commandBuilder.BindBuilders(this._commands);
            this._commandBuilder.Build();
            return this;
        }

      

        public CommandsChain Where(ICommandBuilder param=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }
            this._commands.Add(_commandShemas.Where());
            if (param != null)
            {
                this._commands.Add(param);
            }
            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.Gap()));
            return this;
        }
        public CommandsChain From(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            this._commands.Add(_commandShemas.From(param_));
           
            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Select(ICommandBuilder columns_=null)
        {
            this._commands=new List<ICommandBuilder>();

            this._commands.Add( this._commandShemas.Select());
            this._commands.Add(columns_);
            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
           }
            this._commandBuilder.BindBuilders(this._commands,this._formatGenerator.FromatFromTokenArray(this._commands,_tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }
       
        public CommandsChain Create(ITypeToken param_=null)
        {

            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Create(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();

            return this;
        }
        public CommandsChain Class(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Class(param_));

            this._commandBuilder.BindBuilders(this._commands
                , this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Vertex(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Vertex(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }
        public CommandsChain Extends(ITypeToken param_=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
            }

            this._commands.Add(_commandShemas.Extends(param_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

        public CommandsChain Content(ICommandBuilder param=null)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
           }

            this._commands.Add(_commandShemas.Content(param));
            this._commandBuilder.BindBuilders(this._commands);
            return this;
        }
        
        public CommandsChain Property(ITypeToken class_, ITypeToken property_, ITypeToken type_, ITypeToken mandatory_, ITypeToken notnull_)
        {
            this._commands=new List<ICommandBuilder>();

            if (this._commandBuilder.Tokens != null)
            {
                this._commands.Add(this._commandBuilder);
           }

            this._commands.Add(_commandShemas.Property(class_,property_,type_,mandatory_,notnull_));

            this._commandBuilder.BindBuilders(this._commands, this._formatGenerator.FromatFromTokenArray(this._commands, _tokenMiniFactory.EmptyString()));
            this._commandBuilder.Build();
            return this;
        }

    }


    #region Schemas

    /// <summary>
    /// Base class for shemas building.
    /// Not abstract, because can be used for manual command generation
    /// </summary>
    public class Shemas
    {
        internal ICommandFactory _commandFactory;
        internal IFormatFactory _formatFactory;
        internal ITokenMiniFactory _miniFactory;

        internal ICommandBuilder _commandBuilder;    
        internal IFormatFromListGenerator _formatGenerator;
       
        internal string lastGeneneratedCommand;

        public Shemas(        
            ICommandBuilder commandBuilder
            , IFormatFromListGenerator formatGenerator
            , ITokenMiniFactory miniFactory_)
        {
            _commandBuilder=commandBuilder;
            _formatGenerator=formatGenerator;
            _miniFactory=miniFactory_;
       }

        public Shemas(
            ICommandFactory commandFactory_
            , IFormatFactory formatFactory_
            , ITokenMiniFactory miniFactory_)
        {
            
            _commandFactory=commandFactory_;
            _formatFactory=formatFactory_;
            _miniFactory=miniFactory_;

            _commandBuilder=_commandFactory.CommandBuilder(_miniFactory, _formatFactory);
            _formatGenerator=formatFactory_.FormatGenerator(_miniFactory);
       }

        public void BuildOld(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}
            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuildOld(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        
        public ICommandBuilder GetBuilder()
        {
            ICommandBuilder cb=_commandFactory.CommandBuilder(this._miniFactory,this._formatFactory);
            cb.BindTokens(this._commandBuilder.Tokens);
            cb.BindFormat(this._commandBuilder.FormatPattern);
            return cb.Build();
       }

        public ICommandBuilder BuildFormatNew(List<ICommandBuilder> tokenList_, ITypeToken format)
        {
            this._commandBuilder.BindBuilders(tokenList_, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }
        public ICommandBuilder BuildFormatNew(List<ITypeToken> tokenList_, ITypeToken format)
        {
            this._commandBuilder.BindTokens(tokenList_);
            this._commandBuilder.BindFormat(format);
            this._commandBuilder.Build();
            return GetBuilder();
       }
        public ICommandBuilder BuildNew(List<ICommandBuilder> tokenList_, ITypeToken delimeter)
        {
            this._commandBuilder.BindBuilders(tokenList_, this._formatGenerator.FromatFromTokenArray(
                tokenList_, delimeter));
            return this._commandBuilder.Build();           
       }
        public ICommandBuilder BuildNew(List<ITypeToken> tokenList_, ITypeToken delimeter)
        {
            this._commandBuilder.BindTokens(tokenList_);
            this._commandBuilder.BindFormat(this._formatGenerator.FromatFromTokenArray(
                tokenList_, delimeter));
            this._commandBuilder.Build();
            return GetBuilder();
       }

        public ICommandBuilder AddLeft(ICommandBuilder b1, ICommandBuilder b2, ITypeToken delimeter)
        {
            List<ICommandBuilder> builder=new List<ICommandBuilder>() {b1, b2};
            ITypeToken format=this._formatGenerator.FromatFromTokenArray(builder, delimeter);          
            this._commandBuilder.BindBuilders(builder, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }
        public ICommandBuilder AddRight(ICommandBuilder b1, ICommandBuilder b2, ITypeToken delimeter)
        {
            List<ICommandBuilder> builder=new List<ICommandBuilder>() {b2, b1};
            ITypeToken format=this._formatGenerator.FromatFromTokenArray(builder, delimeter);         
            this._commandBuilder.BindBuilders(builder, format);
            this._commandBuilder.Build();
            return GetBuilder();
       }

   }
    /// <summary>
    /// Shemas for different commands with parameters and builders.
    /// Needs CommandBuilder and FormatGenerator realizations.
    /// Command tokens generated in token factory.
    /// </summary>
    public class CommandShemasExplicit : Shemas
    {

        IOrientQueryFactory _orientFactory;

        //public CommandShemasExplicit(ICommandBuilder commandBuilder_
        //    , IFormatFromListGenerator formatListgenerator_
        //    , ITokenMiniFactory miniFactory_
        //    , IOrientQueryFactory queryfactory_)
        //    :base(commandBuilder_,formatListgenerator_,miniFactory_)
        //{
        //    this._commandBuilder=commandBuilder_;
        //    this._formatGenerator=formatListgenerator_;
        //    this.miniFactory=miniFactory_;
        //    this.orientFactory=queryfactory_;
        //}

        public CommandShemasExplicit(
            ICommandFactory commandFactory_
            , IFormatFactory formatFactory_
            , ITokenMiniFactory miniFactory_
            , IOrientQueryFactory queryfactory_)
            : base(commandFactory_, formatFactory_, miniFactory_)
        {

            this._commandFactory=commandFactory_;
            this._formatFactory=formatFactory_;
            this._miniFactory=miniFactory_;
            this._orientFactory=queryfactory_;

            this._commandBuilder=_commandFactory.CommandBuilder(base._miniFactory, _formatFactory);
            this._formatGenerator=formatFactory_.FormatGenerator(base._miniFactory);
         
       }

        public void Build(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.AddTokens(tokenList_);
            
            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuild(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }


        /// <summary>
        /// For default right handed command functionality (Extends,class,token e.t.c)
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="param_">Command parameters</param>
        /// <returns></returns>
        public void ParametrizedCommand(ITypeToken token_, ICommandBuilder param_)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            List<ITypeToken> tokens=new List<ITypeToken>();

            tokens.Add(token_);
            tokens.Add(_miniFactory.Gap());
            buildersList.Add(BuildNew(tokens, this._miniFactory.EmptyString()));
            if (param_ != null)
            {
                buildersList.Add(param_);               
           }
          
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.EmptyString());          
       }
        public void ParametrizedCommand(List<ITypeToken> token_, ICommandBuilder param_, ITypeToken delimeter_=null)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            ITypeToken placeholder=null;
            if (delimeter_==null) {placeholder=this._miniFactory.EmptyString();}
            else {placeholder=delimeter_;}

            buildersList.Add(BuildNew(token_, placeholder));
            if (param_ != null)
            {
                buildersList.Add(param_);
           }

            this._commandBuilder=BuildNew(buildersList, this._miniFactory.EmptyString());
       }
        public void ParametrizedCommand(ITypeToken token_, ITypeToken param_)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            List<ITypeToken> tokens=new List<ITypeToken>();

            tokens.Add(token_);
            tokens.Add(_miniFactory.Gap());
            if (param_ != null)
            {
                tokens.Add(param_);
                tokens.Add(_miniFactory.Gap());
           }
            buildersList.Add(BuildNew(tokens, this._miniFactory.EmptyString()));
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.Gap());
       }
        public void ParametrizedCommand(List<ITypeToken> tokens_, ITypeToken delimeter_= null)
        {
            List<ICommandBuilder> buildersList=new List<ICommandBuilder>();
            ITypeToken placeholder=null;

            if (delimeter_==null) {placeholder=this._miniFactory.EmptyString();}
            else {placeholder=delimeter_;}

            buildersList.Add(BuildNew(tokens_, placeholder));
            this._commandBuilder=BuildNew(buildersList, this._miniFactory.Gap());
       }

        /// <summary>
        /// For creating commans with nesting functionality and double indenting (Nest).
        /// Adds to the left from all previous commandbuilder
        /// </summary>
        /// <param name="tokenList">List of tokens added to the left</param>
        /// <param name="format_">Format for token assembly. If null standart with gap generated</param>
        /// <returns></returns>
        public ICommandBuilder CommandFormattedRebuild(List<ITypeToken> tokenList, ITypeToken format_)
        {
            ReBuild(tokenList, format_);
            return this._commandBuilder;
       }
        /// <summary>
        /// For creating commans with nesting functionality and left indenting (Content,Create,Where).
        /// Adds to the right from all previous commandbuilder but with parameters
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="builder_">Builder, to which Tokens command is used</param>
        /// <param name="gapBefore">Gap before command presence</param>
        /// <returns></returns>
        public ICommandBuilder ParametrizedCommand(ITypeToken token_, ICommandBuilder builder_, bool gapBefore)
        {
           
            List<ITypeToken> tokenList=new List<ITypeToken>();
            if (gapBefore)
            {
                tokenList.Add(_miniFactory.Gap());
           }
            tokenList.Add(token_);

            if (builder_ != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.AddRange(builder_.Tokens);
           }

            Build(tokenList, _formatGenerator.FromatFromTokenArray(tokenList, _miniFactory.EmptyString()));
            return this._commandBuilder;
       }
        /// <summary>
        /// For creating commans with nesting functionality and left indenting (Select).
        /// Adds to the left from all previous commandbuilder. Has no left gap string
        /// </summary>
        /// <param name="token_">Command token</param>
        /// <param name="params_">Command parameters</param>
        /// <param name="builder_">Builder, to which Tokens command is used</param>
        /// <param name="gapBefore">Gap before command presence</param>
        /// <returns></returns>
        public ICommandBuilder ParametrizedCommandRebuild(ITypeToken token_
            , ICommandBuilder params_, ICommandBuilder builder_,bool gapBefore)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            if (gapBefore)
            {
                tokenList.Add(_miniFactory.Gap());
           }
            tokenList.Add(token_);

            if (params_ != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.AddRange(params_.Tokens);
           }

            if(builder_!=null)
            {
                if(builder_.Tokens !=null)
                {
                    tokenList.Add(_miniFactory.Gap());
                    tokenList.AddRange(builder_.Tokens);                   
               }
           }

            ReBuild(tokenList, _formatGenerator.FromatFromTokenArray(tokenList, _miniFactory.EmptyString()));
            return this._commandBuilder;
       }


        //<<<obsolette possible not used
        public ICommandBuilder CommandFormatted(List<ITypeToken> tokenList, ITypeToken format_)
        {
            Build(tokenList, format_);
            return this._commandBuilder;
       }


        [Obsolete]
        public void Build(List<ITypeToken> tokenList_, IFormatFromListGenerator formatgenerator_)
        {
            tokenList_.Add(_miniFactory.EmptyString());
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            token=formatgenerator_.FromatFromTokenArray(tokenList_);

            this._commandBuilder.AddFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }        
        /// <summary>
        /// Universal builder of tokens, looks like reinvention of concat, but for classes
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Obsolete]
        public ICommandBuilder Command(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.AddTokens(tokenList_);
            this._commandBuilder.AddFormat(token);
            this._commandBuilder.Build();
            return this._commandBuilder;
       }
        /// <summary>
        /// Universal builder of commands use for large commands aggregation
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Obsolete]
        public ICommandBuilder Command(List<ICommandBuilder> tokens, ITypeToken format)
        {
            _commandBuilder.BindBuilders(tokens, format);
            return this._commandBuilder;
       }
        [Obsolete]
        public List<ITypeToken> Tokenise(List<ITypeToken> token_, ITypeToken param=null)
        {
            if (param != null)
            {
                token_.Add(param);
           }

            return token_;
       }
        [Obsolete]
        public List<ITypeToken> Tokenise(ITypeToken token_, ICommandBuilder param)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(token_);
            if (param != null)
            {
                tokenList.AddRange(param.Tokens);
           }
            return tokenList;
       }

        public ICommandBuilder Nest(ICommandBuilder param, ITypeToken leftToken_, ITypeToken rightToken_
          , ITypeToken format=null)
        {
            List<ICommandBuilder> commands_=new List<ICommandBuilder>();
            ITypeToken lt, rt;

            lt=(leftToken_==null) ? _orientFactory.LeftRoundBraket() : leftToken_;
            rt=(rightToken_==null) ? _orientFactory.RightRoundBraket() : rightToken_;

            List<ITypeToken> ltL=new List<ITypeToken> {lt};
            ITypeToken t1=_formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(ltL);
            commands_.Add(_commandFactory.CommandBuilder(_miniFactory, _formatFactory, ltL, t1));

            if (param.Tokens != null || param.Text!=null)
            {
                commands_.Add(param);
           }

            List<ITypeToken> ltR=new List<ITypeToken> {rt};
            ITypeToken t2=_formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(ltL);
            commands_.Add(_commandFactory.CommandBuilder(_miniFactory, _formatFactory, ltR, t2));

            this._commandBuilder.BindBuilders(
                commands_, _formatFactory.FormatGenerator(_miniFactory).FromatFromTokenArray(commands_,_miniFactory.EmptyString())
                );

            return GetBuilder();
            
       }

     

        public ICommandBuilder Content(ICommandBuilder param_)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_=new List<ICommandBuilder>();

            tokens_.Add(_orientFactory.ContentToken());
            tokens_.Add(_miniFactory.Gap());

            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );

            if (param_ != null)
            {
                builders_.Add(param_);
                tokens_=new List<ITypeToken>();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
               _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                   , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString())).Build()
                   );
           }
          
            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();          
       }
        public ICommandBuilder Extends(ITypeToken param)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_orientFactory.ExtendsToken());
            if (param != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
           }

            ParametrizedCommand(tokenList, null);
            return GetBuilder();
            return GetBuilder();
       }
        public ICommandBuilder Class(ITypeToken param=null)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_orientFactory.ClassToken());
            if (param != null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
           }

            ParametrizedCommand(tokenList, null);
            return GetBuilder();           
       }

        public ICommandBuilder Property(ITypeToken class_, ITypeToken prop_, ITypeToken type_
        , ITypeToken mandatory_, ITypeToken notnull_)
        {
            List<ICommandBuilder> builders=new List<ICommandBuilder>();

            builders.Add(PropertyItem(class_, prop_));
            builders.Add(PropertyType(type_));
            if (mandatory_ != null && notnull_ != null)
            {
                builders.Add(PropertyCondition(mandatory_, notnull_));
           }

            return BuildNew(builders, _miniFactory.EmptyString());
       }

        public ICommandBuilder PropertyItem(ITypeToken class_,ITypeToken prop_ )
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_orientFactory.PropertyToken());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(class_);
            tokenList.Add(_miniFactory.Dot());
            tokenList.Add(prop_);
            ParametrizedCommand(tokenList,null);
            return GetBuilder();           
       }
        public ICommandBuilder PropertyType(ITypeToken type_)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(type_);           

            ParametrizedCommand(tokenList, null);
            return GetBuilder();
       }
        public ICommandBuilder PropertyCondition(ITypeToken mandatory_, ITypeToken notnull_)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();

            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_orientFactory.LeftRoundBraket());
            tokenList.Add(_orientFactory.Mandatory());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(mandatory_);
            tokenList.Add(_miniFactory.Coma());
            tokenList.Add(_orientFactory.NotNull());
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(notnull_);
            tokenList.Add(_orientFactory.RightRoundBraket());
          
            ParametrizedCommand(tokenList, null);
            return GetBuilder();
       }

        public ICommandBuilder Vertex(ITypeToken param=null)
        {
            List<ITypeToken> tokenList=new List<ITypeToken>();
            tokenList.Add(_miniFactory.Gap());
            tokenList.Add(_orientFactory.VertexToken());
            if(param!=null)
            {
                tokenList.Add(_miniFactory.Gap());
                tokenList.Add(param);
           }

            ParametrizedCommand(tokenList,null);
            return GetBuilder();
       }
        public ICommandBuilder Edge(ITypeToken param=null)
        {
            ParametrizedCommand(_orientFactory.EdgeToken(), param);
            return GetBuilder();
       }

        public ICommandBuilder Create(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();

            tokens_.Add(_orientFactory.CreateToken());            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);              
           }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
       }

        /// <summary>
        /// Returns select from command when null passed. when any commandbuilderpassed returns select {0} from command
        /// </summary>
        /// <param name="param_">Command builder containining what to select </param>
        /// <returns></returns>
        public ICommandBuilder Select(ICommandBuilder param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_= new List<ICommandBuilder>();

            tokens_.Add(_orientFactory.SelectToken());
            tokens_.Add(_miniFactory.Gap());

            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );

            if (param_ != null)
            {
                builders_.Add(param_);
                tokens_=new List<ITypeToken>();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
               _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                   , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString())).Build()
                   );
           }

            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();

       }       
        public ICommandBuilder Select(List<ITypeToken> param_)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();

            tokens_.Add(_orientFactory.SelectToken());
            tokens_.Add(_miniFactory.Gap());
            if (param_ != null)
            {
                tokens_.AddRange(param_);               
           }
            
            ParametrizedCommand(tokens_, null);
            return GetBuilder();

       }

        public ICommandBuilder From(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_orientFactory.FromToken()); 
            
            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
           }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();

       }

        public ICommandBuilder Where(ICommandBuilder param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            List<ICommandBuilder> builders_=new List<ICommandBuilder>();
          
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_orientFactory.WhereToken());        

            builders_.Add(
            _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );

            if (param_ != null)
            {
                tokens_.Clear();
                tokens_.Add(_miniFactory.Gap());
                builders_.Add(
                _commandFactory.CommandBuilder(_miniFactory, _formatFactory
                , tokens_, _formatGenerator.FromatFromTokenArray(tokens_, _miniFactory.EmptyString()))
                );
                
                builders_.Add(param_);
           }

            this._commandBuilder=BuildNew(builders_, this._miniFactory.EmptyString());
            return GetBuilder();
       }

        public ICommandBuilder To(ITypeToken param_=null)
        {
            List<ITypeToken> tokens_=new List<ITypeToken>();
            tokens_.Add(_miniFactory.Gap());
            tokens_.Add(_orientFactory.ToToken());

            if (param_ != null)
            {
                tokens_.Add(_miniFactory.Gap());
                tokens_.Add(param_);
           }

            ParametrizedCommand(tokens_, null);
            return GetBuilder();
       }      

   }
    /// <summary>
    /// Shemas for Orient URLs
    /// </summary>
    public class UrlShemasExplicit : Shemas
    {
      
        internal IOrientBodyFactory _QueryFactory;

        internal ITypeToken dbHost;

        public UrlShemasExplicit(ICommandBuilder commandBuilder_,
            IFormatFromListGenerator formatGenerator_,
            ITokenMiniFactory miniFactory_,
            IOrientBodyFactory queryFactory_)
            : base(commandBuilder_, formatGenerator_, miniFactory_)
        {
            _commandBuilder=commandBuilder_;
            _formatGenerator=formatGenerator_;
            _miniFactory=miniFactory_;
            _QueryFactory=queryFactory_;           
       }

        public UrlShemasExplicit(
          ICommandFactory commandFactory_
          , IFormatFactory formatFactory_
          , ITokenMiniFactory miniFactory_
            , IOrientBodyFactory queryFactory_)
            : base(commandFactory_,formatFactory_,miniFactory_)
        {
            _QueryFactory=queryFactory_;
       }

        public void AddHost(ITypeToken host_)
        {
            this.dbHost=host_;
       }
        public ITypeToken GetHost()
        {
            return this.dbHost;
       }
        internal void ChekHost()
        {
            if (this.dbHost==null || this.dbHost.Text==null)
            {
                throw new Exception("No db host URL passed");
           }
       }
        public void Build(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.AddTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.AddFormat(token);          
       }
        public void ReBuild(List<ITypeToken> tokenList_, ITypeToken format=null)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (format==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {token=format;}

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }
        public void ReBuildDelimeter(List<ITypeToken> tokenList_, ITypeToken delimeter_)
        {
            //tokenList_.Add(factory.NewEmptyString());            
            this._commandBuilder.BindTokens(tokenList_);

            //generate format from token list with system.empty placeholder List<n> => "{0} ..{}.. {n}"
            ITypeToken token=_miniFactory.NewToken();
            if (delimeter_==null)
            {
                token=_formatGenerator.FromatFromTokenArray(tokenList_);
           }
            else {
                token=_formatGenerator.FromatFromTokenArray(tokenList_, delimeter_);
           }

            this._commandBuilder.BindFormat(token);
            lastGeneneratedCommand=this._commandBuilder.Build().GetText();
       }


        public ICommandBuilder Database(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Database());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.PLocal());

            ReBuildDelimeter(typeToken, new TextToken() {Text=string.Empty});
            return this._commandBuilder;
       }
        public ICommandBuilder Connect(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Connect());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            ReBuildDelimeter(typeToken, new TextToken() {Text=string.Empty});
            return this._commandBuilder;
       }

        public ICommandBuilder Command(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Command());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.sql());

            ReBuildDelimeter(typeToken, _miniFactory.EmptyString());
            return this._commandBuilder;
       }
        public ICommandBuilder Batch(ITypeToken databaseName_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            typeToken.Add(this.dbHost);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.Batch());

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(databaseName_);

            typeToken.Add(this._QueryFactory.BackSlash());
            typeToken.Add(this._QueryFactory.sql());

            ReBuildDelimeter(typeToken, new TextToken() {Text= string.Empty});
            return this._commandBuilder;
       }

   }
    /// <summary>
    /// Shemas for Orient request body parameters command\batch
    /// </summary>
    public class BodyShemas : UrlShemasExplicit
    {

        public BodyShemas(
        ICommandFactory commandFactory_
        , IFormatFactory formatFactory_
        , ITokenMiniFactory miniFactory_
        , IOrientBodyFactory queryBoduFactory_)
        : base(commandFactory_, formatFactory_, miniFactory_, queryBoduFactory_)
        {
            
       }    

        public ICommandBuilder Command(ICommandBuilder command_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            List<ICommandBuilder> commandBuilers=new List<ICommandBuilder>();


            typeToken.Add(this._QueryFactory.LeftFgGap());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Command());
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Colon());
            typeToken.Add(this._QueryFactory.Quotes());

            //build left part of body with no gaps and add to builder list           
            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));
            
            commandBuilers.Add(BuildFormatNew(command_.Tokens, command_.FormatPattern));

            typeToken=new List<ITypeToken>();
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.RightFgGap());

            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            BuildNew(commandBuilers, _miniFactory.EmptyString());               
            return GetBuilder();
       }

        public ICommandBuilder Batch(ICommandBuilder command_)
        {
            ChekHost();
            List<ITypeToken> typeToken=new List<ITypeToken>();
            List<ICommandBuilder> commandBuilers=new List<ICommandBuilder>();
            
            //{
            typeToken.Add(this._QueryFactory.LeftFgGap());

            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Transactions());
            typeToken.Add(this._QueryFactory.Quotes());

            //:
            typeToken.Add(this._QueryFactory.Colon());

            typeToken.Add(this._QueryFactory.True());
            typeToken.Add(this._QueryFactory.Comma());

            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.Operations());
            typeToken.Add(this._QueryFactory.Quotes());

            //:
            typeToken.Add(this._QueryFactory.Colon());
                
                //{[
                typeToken.Add(this._QueryFactory.LeftSqGap());
                typeToken.Add(this._QueryFactory.LeftFgGap());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Type());
                    typeToken.Add(this._QueryFactory.Quotes());
                    //:
                    typeToken.Add(this._QueryFactory.Colon());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Sctipt());
                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Comma());


                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Language());
                    typeToken.Add(this._QueryFactory.Quotes());
                    //:
                    typeToken.Add(this._QueryFactory.Colon());

                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.sql());
                    typeToken.Add(this._QueryFactory.Quotes());
                    typeToken.Add(this._QueryFactory.Comma());

                        typeToken.Add(this._QueryFactory.Quotes());
                        typeToken.Add(this._QueryFactory.Sctipt());
                        typeToken.Add(this._QueryFactory.Quotes());
                        //:
                        typeToken.Add(this._QueryFactory.Colon());
                        
                        //["
                        typeToken.Add(this._QueryFactory.LeftSqGap());
                        typeToken.Add(this._QueryFactory.Quotes());

            //build left part of body with no gaps and add to builder list
            commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

            //build command preserving with command format pattern gaps and add to builder list
            commandBuilers.Add(
        BuildFormatNew(command_.Tokens, command_.FormatPattern)
    );

    typeToken=new List<ITypeToken>();

            //"]}]}
            typeToken.Add(this._QueryFactory.Quotes());
            typeToken.Add(this._QueryFactory.RightSqGap());
            typeToken.Add(this._QueryFactory.RightFgGap());
            typeToken.Add(this._QueryFactory.RightSqGap());            
            typeToken.Add(this._QueryFactory.RightFgGap());

    //build left part of body with no gaps and add to builder list   
    commandBuilers.Add(BuildNew(typeToken, _miniFactory.EmptyString()));

    BuildNew(commandBuilers, _miniFactory.Gap());
    return GetBuilder();
       }

   }
    
    #endregion


    //<<<
    /// <summary>
    /// <<< deprecation possible. mmostly replaced by generator with empty string placeholder. 
    /// Complex edge filter formats replaced by groups of nested concatenation
    /// </summary>
    #region TokenFormats
    //Auth Orient URL
    public class OrientAuthenticationURLFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}:{1}/{2}/{3}";
   }
    //Command URL part format
    public class OrientCommandURLFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}:{1}/{2}/{3}/{4}"; 
   }
    public class OrientDatabaseUrlFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}/{1}";
   }


    /// </summary>
    /// command queries contains prevoius command as first parameter, 
    /// cause WHERE not intended to be used without select
    /// </summary>

    //command query part format
    public class OrientSelectClauseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2}";
   } 
    //Command for concatenating select command and where clause
    public class OrientWhereClauseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1}";
   } 
    //create vertex command Format
    public class OrientCreateVertexCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2} {3} {4}"; 
   }
    //delete vertex command Format
    public class OrientDeleteVertexCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2} {3}";
   }
    //delete command Format
    public class OrientDeleteCluaseFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0} {1} {2}"; 
   }
    //format for inEoutV select
    public class OrientOutEinVFormat : ITypeToken
    {
        public string Text {  get; set;}=@"{0} {1}{2} {3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18} {19} {20} {21} {22}"; 
   }
    public class OrientOutVinVFormat : ITypeToken
    {
        public string Text {get; set;}=@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12} {13} {14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}"; 
   }
    #endregion
    //<<<
    #region CommandBuilders

    /// <summary>
    /// Builders.
    /// Build command acording to type of passed object (class,vertes, or edge with objects referenced or ids)
    /// Not use predefined formatters 
    /// for special commands like create class/edge/vertex
    /// which not requer special format like {0}:{1}\{2} but samle , generated fro mtoken list like {0} {1} {2}
    /// but generated in lagre ammounts with differen types.
    /// </summary>
    public class OrientTokenBuilder : ITokenBuilder
    {
        //for select command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(new OrientFromToken());
            result.Add(orientObject);
            return result;
       }

        //for delete, select conditional command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            if (command_ is OrientDeleteToken)
            {
                result.Add(command_);
                result.Add(orientType);
                //result.Add(new OrientFromToken());
                result.Add(orientObject);
           }
            if (command_ is OrientSelectToken)
            {
                result.Add(command_);
                result.Add(new OrientFromToken());
                result.Add(orientObject);
                result.Add(new OrientWhereToken());
                result.Add(orientType);
           }
            return result;
       }

        //for create command, authenticate, command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken context_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientContentToken());
            result.Add(context_);
            return result;
       }

        //for create Edge from to 
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken tokenA, ITypeToken tokenB, ITypeToken context_)
        {
            tokenA.Text=tokenA.Text.Replace(@"#", "");
            tokenB.Text=tokenB.Text.Replace(@"#", "");

            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientFromToken());
            result.Add(tokenA);
            result.Add(new OrientToToken());
            result.Add(tokenB);

            if (command_ is OrientCreateToken)
            {
                if (context_ != null && context_.Text != null && context_.Text != string.Empty)
                {
                    result.Add(new OrientContentToken());
                    result.Add(context_);
               }
           }
            if (command_ is OrientDeleteToken)
            {
                if (context_ != null && context_.Text != null && context_.Text != string.Empty)
                {
                    result.Add(context_);
               }
           }
            return result;
       }

   }

    public class OreintNewsTokenBuilder
    {
        TypeConverter typeConverter_=new TypeConverter();

        public List<ITypeToken> outEinVExp(ITypeToken command_, ITypeToken vertex_, ITypeToken edge_, ITypeToken condition_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            //expand
            result.Add(new OrientExpandToken());
            result.Add(new OrientRoundBraketLeftToken());

            //outE
            result.Add(new OrientOutToken());
            result.Add(new OrientEToken());
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(edge_);
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());
            result.Add(new OrientDotToken());
            //inv
            result.Add(new OrientInToken());
            result.Add(new OrientVToken());
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(vertex_);
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());

            result.Add(new OrientRoundBraketRightToken());

            result.Add(new OrientFromToken());
            result.Add(vertex_);

            if (condition_ != null && condition_.Text != null && condition_.Text != string.Empty)
            {
                result.Add(new OrientWhereToken());
                result.Add(condition_);
           }

            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientApostropheToken());
            result.Add(new OrientRoundBraketRightToken());
            return result;
       }

        public List<ITypeToken> outVinVcnd(Type object_, ITypeToken property_, ITypeToken conda_, ITypeToken condb_)
        {
            List<ITypeToken> result=new List<ITypeToken>();

            if (object_.BaseType==typeof(OrientVertex) || object_.BaseType==typeof(OrientEdge))
            {

                //outV
                result.Add(new OrientOutToken());
                result.Add(new OrientVToken());
                result.Add(new OrientRoundBraketLeftToken());
                result.Add(new OrientApostropheToken());
                result.Add(typeConverter_.Get(object_));
                result.Add(new OrientApostropheToken());
                result.Add(new OrientRoundBraketRightToken());
                result.Add(new OrientDotToken());
                result.Add(property_);

                result.Add(new OrientEqualsToken());

                result.Add(new OrientApostropheToken());
                result.Add(conda_);
                result.Add(new OrientApostropheToken());

                result.Add(new OrientAndToken());

                //inv
                result.Add(new OrientInToken());
                result.Add(new OrientVToken());
                result.Add(new OrientRoundBraketLeftToken());
                result.Add(new OrientApostropheToken());
                result.Add(typeConverter_.Get(object_));
                result.Add(new OrientApostropheToken());
                result.Add(new OrientRoundBraketRightToken());
                result.Add(new OrientDotToken());
                result.Add(property_);

                result.Add(new OrientEqualsToken());

                result.Add(new OrientApostropheToken());
                result.Add(condb_);
                result.Add(new OrientApostropheToken());

           }
            return result;
       }

   }
    /// <summary>creates collection of tokens
    /// builds add,delete,create commands from token amount
    /// </summary>
    public class OrientCommandBuilderImplicit : ITokenBuilderTypeGen
    {

        ITypeConverter _typeConverter;

        public void BindConverter(ITypeConverter typecOnverter_)
        {
            this._typeConverter=typecOnverter_;
       }

        public List<ITypeToken> Command(ITypeToken name_, ITypeToken type_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(name_);
            result.Add(type_);
            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));


            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            if (content != null)

            {
                if (orientClass_ is IOrientClass)
                {
                    result.Add(new OrientExtendsToken());
               }
                if (orientClass_ is IOrientVertex)
                {
                    result.Add(new OrientContentToken());
               }



                result.Add(content);
           }
            return result;
       }
        public List<ITypeToken> Command(ITypeToken command_, Type orientClass_, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            if (content != null)

            {
                if (_typeConverter.GetBase(orientClass_.GetType()) is IOrientClass)
                {
                    result.Add(new OrientExtendsToken());
               }
                if (_typeConverter.GetBase(orientClass_.GetType()) is IOrientVertex)
                {
                    result.Add(new OrientContentToken());
               }




                result.Add(content);
           }
            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, IOrientObject orientProperty_, ITypeToken orientType_, bool mandatory=false, bool notnull=false)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientProperty_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));
            result.Add(new OrientDotToken());
            result.Add(_typeConverter.Get(orientProperty_.GetType()));
            result.Add(orientType_);
            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientMandatoryToken());
            if (mandatory)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}
            result.Add(new OrientNotNULLToken());

            result.Add(new OrientCommaToken());

            if (notnull)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}
            result.Add(new OrientRoundBraketRightToken());





            return result;
       }


        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken from, ITypeToken to, ITypeToken content=null)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(command_);
            result.Add(_typeConverter.GetBase(orientClass_.GetType()));
            result.Add(_typeConverter.Get(orientClass_.GetType()));


            result.Add(new OrientFromToken());
            result.Add(from);
            result.Add(new OrientToToken());
            result.Add(to);
            if (content != null)
            {
                result.Add(new OrientContentToken());
                result.Add(content);
           }

            return result;
       }

   }

    /// <summary>
    /// Builder with exlicitly named commands
    /// </summary>
    public class OrientTokenBuilderExplicit
    {

        //Create class cluase (type check) with extends class option
        public List<ITypeToken> Create(OrientClassToken classType_, ITypeToken extendsClassType_=null)
        {

            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientCreateToken()
           };

            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientClassToken());
                result.Add(classType_);

                if (extendsClassType_ != null)
                {
                    result.Add(new OrientExtendsToken());
                    result.Add(extendsClassType_);
               }
           }
            return result;
       }
        //Create  vertex cluase (type check) with content optiona
        public List<ITypeToken> Create(OrientVertexToken classType_, ITypeToken extendsClassType_=null)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientCreateToken()
           };
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientVertexToken());
                result.Add(classType_);

                if (extendsClassType_ != null)
                {
                    result.Add(new OrientContentToken());
                    result.Add(extendsClassType_);
               }
           }
            return result;
       }
        //Create property cluase
        public List<ITypeToken> Create(ITypeToken className_, ITypeToken propertyName_, ITypeToken propertyType_,
        bool mandatory_=false, bool notnull=false)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientCreateToken(), new OrientPropertyToken(),className_, new OrientDotToken(),propertyName_,propertyType_
           };

            result.Add(new OrientRoundBraketLeftToken());
            result.Add(new OrientMandatoryToken());

            if (mandatory_)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}

            result.Add(new OrientCommaToken());
            result.Add(new OrientNotNULLToken());

            if (notnull)
            {
                result.Add(new OrientTRUEToken());
           }
            else {result.Add(new OrientFLASEToken());}

            result.Add(new OrientRoundBraketRightToken());

            return result;
       }
        //Create edge clause
        public List<ITypeToken> Create(ITypeToken className_, ITypeToken from_, ITypeToken to_)
        {

            List<ITypeToken> result=new List<ITypeToken>(){
                new OrientCreateToken(), new OrientEdgeToken(), className_, new OrientFromToken(),
                from_,
                new OrientToToken(),
                to_
           };

            return result;
       }
        //For select from cluases (Vertex,edge)
        public List<ITypeToken> Select(ITypeToken orientType_, ITypeToken class_)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientSelectToken(),orientType_, new OrientFromToken(), class_
           };

            return result;
       }
        //Where clauses receiveing content< which must be strictly parametrized in UOW
        public List<ITypeToken> Where(ITypeToken orientType_, ITypeToken content_)
        {
            List<ITypeToken> result=new List<ITypeToken>() {
                new OrientWhereToken(),content_
           };

            return result;
       }
        //delete vertex,edge,or class
        public List<ITypeToken> Delete(ITypeToken classType_, ITypeToken classname_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientDeleteToken());
                result.Add(classType_);
           }
            if (classType_ is OrientEdgeToken)
            {
                result.Add(new OrientDeleteToken());
                result.Add(classType_);
           }
            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientDropToken());
                result.Add(classType_);
           }
            if (result.Count() != 0)
            {
                result.Add(classname_);
           }
            return result;
       }

        public List<ITypeToken> Function(ITypeToken function_, ICommandBuilder params_)
        {
            List<ITypeToken> result=new List<ITypeToken>();
            result.Add(function_);
            result.AddRange(params_.Tokens);
            return result;
       }
   }

    #endregion
    //<<<
    #region TokenListsBuilders

    ///<summary>buider for commands with format
    ///mostly used for URLS (auth)</summary
    public class OrientCommandBuilder : CommandBuilder
    {
      
        public OrientCommandBuilder(ITokenMiniFactory tokenFactory_,  IFormatFactory formatFactory_) 
            : base(tokenFactory_, formatFactory_)
        {

       }
       
   }

    //<<<deprecation possible, replaced with type convertible commandbuilder
    ///<summary>   
    ///Query builders
    ///class segregation for different cluse builders
    ///</summary>
    //Authentication URL build
    public class OrientAuthenticationURIBuilder : CommandBuilder
    {
        public OrientAuthenticationURIBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    //Command URL build
    public class OrientCommandURIBuilder : CommandBuilder
    {
        public OrientCommandURIBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
        public OrientCommandURIBuilder(List<ICommandBuilder> texts_, ITypeToken FormatPattern_)
          : base(texts_,FormatPattern_)
        {

       }
   }

    public class OrientSelectClauseBuilder : CommandBuilder
    {
        public OrientSelectClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    public class OrientWhereClauseBuilder : CommandBuilder
    {
        public OrientWhereClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }

    public class OrientCreateClauseBuilder : CommandBuilder
    {
        public OrientCreateClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_) {

       }
   }
    public class OrientDeleteClauseBuilder : CommandBuilder
    {
        public OrientDeleteClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }

    public class OrientNestedSelectClauseBuilder : CommandBuilder
    {
        public OrientNestedSelectClauseBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatfactory_)
             : base(tokenFactory_, formatfactory_)
        {

       }
   }
    #endregion
    //<<<
    ///<summary>predefined url token collections
    ///prefered change to predefined url and command builds</summary>

    public static class TokenRepo
    {
        public static List<ITypeToken> authUrl=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientAuthenticateToken(), new OrientDatabaseNameToken()};
        public static List<ITypeToken> commandUrl=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientCommandToken(), new OrientDatabaseNameToken(), new OrientCommandSQLTypeToken()};
        public static List<ITypeToken> addDbURL=new List<ITypeToken>() {new OrientHost(), new OrientPort(), new OrientURLDatabaseToken()};
    }
	
    ///<summary>Converts from model poco classes types to ItypeToken types
    ///</summary>
    public class TypeConverter : ITypeConverter
    {

        Dictionary<Type, ITypeToken> types;

        public TypeConverter()
        {

            types=new Dictionary<Type, ITypeToken>();

            types.Add(typeof(OrientVertex), new OrientVertexToken());
            types.Add(typeof(OrientEdge), new OrientEdgeToken());

            types.Add(typeof(Person), new OrientPersonToken());
            types.Add(typeof(Unit), new OrientUnitToken());

            types.Add(typeof(MainAssignment), new OrientMainAssignmentToken());
            types.Add(typeof(SubUnit), new OrientSubUnitToken());

            types.Add(typeof(UserSettings), new OrientUserSettingsToken());
            types.Add(typeof(CommonSettings), new OrientCommonSettingsToken());
            
            types.Add(typeof(TrackBirthdays), new OrientTrackBirthdaysToken());
        }
        public void Add(Type type_, ITypeToken token_)
        {
            this.types.Add(type_, token_);
        }
        public ITypeToken Get(Type type_)
        {
            ITypeToken token_=null;

            types.TryGetValue(type_, out token_);

            return token_;
        }
        public ITypeToken GetBase(Type type_)
        {
            ITypeToken token_=null;
            Type tp=type_.BaseType;
            types.TryGetValue(tp, out token_);

            return token_;
        }
        public ITypeToken Get(IOrientObject object_)
        {
            ITypeToken token_=null;

            types.TryGetValue(object_.GetType(), out token_);

            return token_;
        }
        public ITypeToken GetBase(IOrientObject object_)
        {
            ITypeToken token_=null;
            Type tp=object_.GetType().BaseType;
            IOrientVertex t2=(IOrientVertex)object_;

            types.TryGetValue(object_.GetType().BaseType, out token_);

            return token_;
        }

    }
    
}