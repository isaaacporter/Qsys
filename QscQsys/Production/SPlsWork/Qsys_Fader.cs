using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;
using QscQsys;
using Crestron.SimplSharp.SimplSharpExtensions;
using TCP_Client;

namespace UserModule_QSYS_FADER
{
    public class UserModuleClass_QSYS_FADER : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        Crestron.Logos.SplusObjects.DigitalInput MUTEON;
        Crestron.Logos.SplusObjects.DigitalInput MUTEOFF;
        Crestron.Logos.SplusObjects.DigitalInput MUTETOGGLE;
        Crestron.Logos.SplusObjects.DigitalInput VOLUMEUP;
        Crestron.Logos.SplusObjects.DigitalInput VOLUMEDOWN;
        Crestron.Logos.SplusObjects.AnalogInput VOLUME;
        Crestron.Logos.SplusObjects.DigitalOutput MUTEISON;
        Crestron.Logos.SplusObjects.DigitalOutput MUTEISOFF;
        Crestron.Logos.SplusObjects.AnalogOutput VOLUMEVALUE;
        QscQsys.QsysFader FADER;
        StringParameter COREID;
        StringParameter COMPONENTNAME;
        UShortParameter VOLUMESTEP;
        UShortParameter VOLUMEREPEATTIME;
        ushort CURRENTVOLUME = 0;
        ushort NEWDIRECTION = 0;
        private void VOLUMEREPEAT (  SplusExecutionContext __context__, ushort DIRECTION ) 
            { 
            
            __context__.SourceCodeLine = 27;
            NEWDIRECTION = (ushort) ( DIRECTION ) ; 
            __context__.SourceCodeLine = 29;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (NEWDIRECTION == 1) ) && Functions.TestForTrue ( Functions.BoolToInt ( CURRENTVOLUME <= (65535 - VOLUMESTEP  .Value) ) )) ))  ) ) 
                {
                __context__.SourceCodeLine = 30;
                FADER . Volume ( (int)( (CURRENTVOLUME + VOLUMESTEP  .Value) )) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 31;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (NEWDIRECTION == 1) ) && Functions.TestForTrue ( Functions.BoolToInt ( CURRENTVOLUME > (65535 - VOLUMESTEP  .Value) ) )) ))  ) ) 
                    {
                    __context__.SourceCodeLine = 32;
                    FADER . Volume ( (int)( 65535 )) ; 
                    }
                
                else 
                    {
                    __context__.SourceCodeLine = 33;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (NEWDIRECTION == 0) ) && Functions.TestForTrue ( Functions.BoolToInt ( CURRENTVOLUME >= (0 + VOLUMESTEP  .Value) ) )) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 34;
                        FADER . Volume ( (int)( (CURRENTVOLUME - VOLUMESTEP  .Value) )) ; 
                        }
                    
                    else 
                        {
                        __context__.SourceCodeLine = 35;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (NEWDIRECTION == 0) ) && Functions.TestForTrue ( Functions.BoolToInt ( CURRENTVOLUME < (0 + VOLUMESTEP  .Value) ) )) ))  ) ) 
                            {
                            __context__.SourceCodeLine = 36;
                            FADER . Volume ( (int)( 0 )) ; 
                            }
                        
                        }
                    
                    }
                
                }
            
            __context__.SourceCodeLine = 38;
            CreateWait ( "VREPEAT" , VOLUMEREPEATTIME  .Value , VREPEAT_Callback ) ;
            
            }
            
        public void VREPEAT_CallbackFn( object stateInfo )
        {
        
            try
            {
                Wait __LocalWait__ = (Wait)stateInfo;
                SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
                __LocalWait__.RemoveFromList();
                
            
            __context__.SourceCodeLine = 40;
            VOLUMEREPEAT (  __context__ , (ushort)( NEWDIRECTION )) ; 
            
        
        
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            
        }
        
    object MUTEON_OnPush_0 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 46;
            FADER . Mute ( (ushort)( 1 )) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object MUTEOFF_OnPush_1 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 51;
        FADER . Mute ( (ushort)( 0 )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MUTETOGGLE_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 56;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.Not( MUTEISON  .Value ) ) && Functions.TestForTrue ( MUTEISOFF  .Value )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 58;
            FADER . Mute ( (ushort)( 1 )) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 60;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( MUTEISON  .Value ) && Functions.TestForTrue ( Functions.Not( MUTEISOFF  .Value ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 62;
                FADER . Mute ( (ushort)( 0 )) ; 
                } 
            
            }
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object VOLUMEUP_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 68;
        VOLUMEREPEAT (  __context__ , (ushort)( 1 )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object VOLUMEUP_OnRelease_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 73;
        CancelWait ( "VREPEAT" ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object VOLUMEDOWN_OnPush_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 78;
        VOLUMEREPEAT (  __context__ , (ushort)( 0 )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object VOLUMEDOWN_OnRelease_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 83;
        CancelWait ( "VREPEAT" ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object VOLUME_OnChange_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort X = 0;
        
        
        __context__.SourceCodeLine = 90;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (VOLUME  .UshortValue == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 92;
            FADER . Volume ( (int)( 0 )) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 96;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt (X != VOLUME  .UshortValue))  ) ) 
                { 
                __context__.SourceCodeLine = 98;
                X = (ushort) ( VOLUME  .UshortValue ) ; 
                __context__.SourceCodeLine = 99;
                FADER . Volume ( (int)( X )) ; 
                __context__.SourceCodeLine = 96;
                } 
            
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public void ONVOLUMECHANGE ( ushort VALUE ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
        
        __context__.SourceCodeLine = 106;
        CURRENTVOLUME = (ushort) ( VALUE ) ; 
        __context__.SourceCodeLine = 107;
        VOLUMEVALUE  .Value = (ushort) ( CURRENTVOLUME ) ; 
        
        
    }
    finally { ObjectFinallyHandler(); }
    }
    
public void ONMUTECHANGE ( ushort VALUE ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
        
        __context__.SourceCodeLine = 112;
        
            {
            int __SPLS_TMPVAR__SWTCH_1__ = ((int)VALUE);
            
                { 
                if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 1) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 116;
                    MUTEISOFF  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 117;
                    MUTEISON  .Value = (ushort) ( 1 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 0) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 121;
                    MUTEISOFF  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 122;
                    MUTEISON  .Value = (ushort) ( 0 ) ; 
                    } 
                
                } 
                
            }
            
        
        
        
    }
    finally { ObjectFinallyHandler(); }
    }
    
public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 129;
        // RegisterDelegate( FADER , NEWVOLUMECHANGE , ONVOLUMECHANGE ) 
        FADER .newVolumeChange  = ONVOLUMECHANGE; ; 
        __context__.SourceCodeLine = 130;
        // RegisterDelegate( FADER , NEWMUTECHANGE , ONMUTECHANGE ) 
        FADER .newMuteChange  = ONMUTECHANGE; ; 
        __context__.SourceCodeLine = 131;
        FADER . Initialize ( COREID  .ToString(), COMPONENTNAME  .ToString()) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    
    MUTEON = new Crestron.Logos.SplusObjects.DigitalInput( MUTEON__DigitalInput__, this );
    m_DigitalInputList.Add( MUTEON__DigitalInput__, MUTEON );
    
    MUTEOFF = new Crestron.Logos.SplusObjects.DigitalInput( MUTEOFF__DigitalInput__, this );
    m_DigitalInputList.Add( MUTEOFF__DigitalInput__, MUTEOFF );
    
    MUTETOGGLE = new Crestron.Logos.SplusObjects.DigitalInput( MUTETOGGLE__DigitalInput__, this );
    m_DigitalInputList.Add( MUTETOGGLE__DigitalInput__, MUTETOGGLE );
    
    VOLUMEUP = new Crestron.Logos.SplusObjects.DigitalInput( VOLUMEUP__DigitalInput__, this );
    m_DigitalInputList.Add( VOLUMEUP__DigitalInput__, VOLUMEUP );
    
    VOLUMEDOWN = new Crestron.Logos.SplusObjects.DigitalInput( VOLUMEDOWN__DigitalInput__, this );
    m_DigitalInputList.Add( VOLUMEDOWN__DigitalInput__, VOLUMEDOWN );
    
    MUTEISON = new Crestron.Logos.SplusObjects.DigitalOutput( MUTEISON__DigitalOutput__, this );
    m_DigitalOutputList.Add( MUTEISON__DigitalOutput__, MUTEISON );
    
    MUTEISOFF = new Crestron.Logos.SplusObjects.DigitalOutput( MUTEISOFF__DigitalOutput__, this );
    m_DigitalOutputList.Add( MUTEISOFF__DigitalOutput__, MUTEISOFF );
    
    VOLUME = new Crestron.Logos.SplusObjects.AnalogInput( VOLUME__AnalogSerialInput__, this );
    m_AnalogInputList.Add( VOLUME__AnalogSerialInput__, VOLUME );
    
    VOLUMEVALUE = new Crestron.Logos.SplusObjects.AnalogOutput( VOLUMEVALUE__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( VOLUMEVALUE__AnalogSerialOutput__, VOLUMEVALUE );
    
    VOLUMESTEP = new UShortParameter( VOLUMESTEP__Parameter__, this );
    m_ParameterList.Add( VOLUMESTEP__Parameter__, VOLUMESTEP );
    
    VOLUMEREPEATTIME = new UShortParameter( VOLUMEREPEATTIME__Parameter__, this );
    m_ParameterList.Add( VOLUMEREPEATTIME__Parameter__, VOLUMEREPEATTIME );
    
    COREID = new StringParameter( COREID__Parameter__, this );
    m_ParameterList.Add( COREID__Parameter__, COREID );
    
    COMPONENTNAME = new StringParameter( COMPONENTNAME__Parameter__, this );
    m_ParameterList.Add( COMPONENTNAME__Parameter__, COMPONENTNAME );
    
    VREPEAT_Callback = new WaitFunction( VREPEAT_CallbackFn );
    
    MUTEON.OnDigitalPush.Add( new InputChangeHandlerWrapper( MUTEON_OnPush_0, false ) );
    MUTEOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( MUTEOFF_OnPush_1, false ) );
    MUTETOGGLE.OnDigitalPush.Add( new InputChangeHandlerWrapper( MUTETOGGLE_OnPush_2, false ) );
    VOLUMEUP.OnDigitalPush.Add( new InputChangeHandlerWrapper( VOLUMEUP_OnPush_3, false ) );
    VOLUMEUP.OnDigitalRelease.Add( new InputChangeHandlerWrapper( VOLUMEUP_OnRelease_4, false ) );
    VOLUMEDOWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( VOLUMEDOWN_OnPush_5, false ) );
    VOLUMEDOWN.OnDigitalRelease.Add( new InputChangeHandlerWrapper( VOLUMEDOWN_OnRelease_6, false ) );
    VOLUME.OnAnalogChange.Add( new InputChangeHandlerWrapper( VOLUME_OnChange_7, true ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    FADER  = new QscQsys.QsysFader();
    
    
}

public UserModuleClass_QSYS_FADER ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}


private WaitFunction VREPEAT_Callback;


const uint MUTEON__DigitalInput__ = 0;
const uint MUTEOFF__DigitalInput__ = 1;
const uint MUTETOGGLE__DigitalInput__ = 2;
const uint VOLUMEUP__DigitalInput__ = 3;
const uint VOLUMEDOWN__DigitalInput__ = 4;
const uint VOLUME__AnalogSerialInput__ = 0;
const uint MUTEISON__DigitalOutput__ = 0;
const uint MUTEISOFF__DigitalOutput__ = 1;
const uint VOLUMEVALUE__AnalogSerialOutput__ = 0;
const uint COREID__Parameter__ = 10;
const uint COMPONENTNAME__Parameter__ = 11;
const uint VOLUMESTEP__Parameter__ = 12;
const uint VOLUMEREPEATTIME__Parameter__ = 13;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
