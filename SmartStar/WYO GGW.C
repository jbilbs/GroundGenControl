/******************************************************************************/
// 							Weather Modification, Inc.
// 				  Remote Ground Genenerator / Weather Station
//                    	 Smart Star - Rabbit 2000
//
//							 Ground Generator Version 2.0
// 								   August 8, 2005
//							 Last updated: March 8, 2007
//									Firmware Version 1.56
//
//								Author:	Raylin J. Nevland
//    					Email:	Raylin.Nevland@ndsu.edu
//										rnevland@weathermod.com
//
/*******************************************************************************
Latest Updates:

01/16/2006	- Weather Station Detection Functional
				- Firmware update rev 1.29
03/14/2006	- Weather Data Parsing Modified for Consistancy
				- Firmware update rev 1.30
03/15/2006 	- Updated Flame Detection Method to Check:
						Temperature Sensors
                  5 Minute Timeout
                  Flame Temperature *new feature*
               *dramatically decreases system start up time.
				- Added igniter sequence to shutdown operations
            	*helps burn off the remaining propane/solution in the lines after
               the solenoids have been closed.
            - Extensive testing on both test bench unit and WMI test generator.
              All tests indicate the unit performed as expect with a
              considerable increase in system startup times.
            - Firmware update rev 1.31
03/18/2006 	- Updated commString to include new pressure sensor and firmware
				  version indication.  Pressure will be displayed graphically next
              to the flow rate and firmware version will show in place of
              'bytes recieved' in the left hand status column.
            - New updates to interface include weather station "variable" string
              size update, weather station now 100% operational, ready for
              deployment in field.
            - Firmware update rev 1.32
03/28/2006  - Updated firmware to include newly interfaced pressure transducer.
				  Transducer reads 0-50 PSI from 4-20mA with minimal hysterisis
              during pressure changes.
            - Updated commString format to include entire range of pressure
              values.
            - Bench unit tested against GUI, updates pressure values as expected.
            - Reprogrammed and tested WMI Test Generator. Varied pressure - Unit
              performed as expected, notably consistant and precise measurments.
            - Firmware update rev 1.33
10/27/2006  - Seeding Failsafe added by Scott Brause.
				- Firmware update rev 1.40
01/02/2007  - Update flame threshold from 400 to 150 degrees.
            - Firmware update rev 1.50
01/09/2007  - Added Safety Self-Monitoring - System Shutdown
					- Battery Voltage Threshold - 10V
               - Tank Pressure Threshold - 10PSI
               - Flame Temperature Threshold - 75F
               - Timeout Threshold - 4hr
            - Corrected Flame Temperature Display
            	- Solved Partial Status String Issue
            - Fail Safe Timeout Reset Bug Fixed
            - Firmware update rev 1.51
01/09/2007  - Fixed 'Keep Alive' bug when connected to interface
				- Fixed invalid case issue
				- Firmwareupdate rev 1.52
02/19/2007  - Weather Station Data Collection Methods Added
				- Firmwareupdate rev 1.53
03/08/2007  - Weather Station Data Collection Methods Optimized
				- No Flame Timeout set to 3 min from 5min
            - Flame Temp Confirm Value set to 150 Deg.
            - Wx Staion MUST be timed for:
            	- 0R1 (Wind Params) 2 second intervals
               - 0R2 (Temp, Rel. Humidity) 5 second intervals
               - Will not communicate correctly if intervals are smaller
				- Firmwareupdate rev 1.55
03/08/2007  - Weather Station intervals set to:
					- 10 minute records
               - 5 minute recent update
				- Firmwareupdate rev 1.56
********************************************************************************
    !Testing Purposes Only!
HyperTerminal Settings Check List:
 1.) 9600 	baud 8N1
 2.) Wrap lines that exceed terminal width
 3.) Append line feeds to incoming line ends
*******************************************************************************/



#class auto
#define DINBUFSIZE  15 // serial buffer size
#define DOUTBUFSIZE 15
#define CINBUFSIZE  15
#define COUTBUFSIZE 15
#ifndef _232BAUD
#define _232BAUD 9600  // serial baud rate
#define WX_STRING_SIZE	78

#define WX_SAMPLING_INTERVAL_MINUTES	5

#define WX_HISTORY_INTERVAL_MINUTES		10
#define WX_MAX_SAMPLE_HOURS				8
#define WX_MAX_SAMPLES					   (60/WX_HISTORY_INTERVAL_MINUTES)*WX_MAX_SAMPLE_HOURS
//#define        WX_MAX_SAMPLES 10

#endif

//
//	Constants
//

//	# of secondds before seeding is terminated without input (4 hrs)
#define	SHUTDOWN_SEEDING_INTERVAL	4*60*60

//	Common error strings
#define	ERROR_WX_NOT_AVAIL	"W-"

/////////////////////////////////FUNCTIONS//////////////////////////////////////
// function prototypes
void buildString ();
void sdelay (long sd);
void getInput ();
void display(int nGetInput);
void errorMessage (int x);
void variableInit ();
void calcData ();
void status ();
void burner ();
void nozzle ();
void solution ();
void closeAll ();
void ignite ();
void purge ();
void sensor ();
void seedStart();
void seedStop();
void parseWeatherString();
void buildWeatherString();
void getWeatherStatus ();
void storeCurrentWeatherString();
void getCurrentWeatherString();
void storeWXNotAvailable();
int isWeatherStationAvailable();

void groundGenControl();
void getWeatherLoop();
void seedingShutdownCheck();

char codeVersion[] = "1.56"; //Smart Star Firmware Version


int statusS1, statusS2, statusS5, seedStatus, statusPurge, statusIgnite,
 			tempSensor, tempSensorCheck, start, temp, choice, battLowVolts;
float flameTemp, battVolts, flowMeter, pressureMeter, flameThreshold;
char commString[96], month[5], day[5], year[5], hour[5], min[5], sec[5];
struct tm      rtc;   // time struct
unsigned long int lRefseconds;



//WEATHER STATION PARAMETERS
char weatherString[WX_STRING_SIZE], weatherStatus[31], avewindDirection[10], avewindSpeed[10],
			relHumidity[10], airPressure[10], airTemp[10], heaterStatus[2], stringType;

char szCurrentWeatherString[WX_STRING_SIZE];
char wxStorageArea[WX_MAX_SAMPLES][WX_STRING_SIZE];
int  wxSampleIndex;
int  wxInitialSampleTaken;
int  nLastMinute;
int  nInitialized;
int  nWxStationAvailable;




int getOk, flag1, flag2, flag3, whileFlag;
char * startTemperature;
char * endTemperature;
char * startHumidity;
char * endHumidity;
char * startPressure;
char * endPressure;
char * startSpeed;
char * endSpeed;
char * startDirection;
char * endDirection;
char * endHeater;

/******************************MAIN PROGRAM************************************/

/// <summary>
/// Main thread program entry point
/// </summary>

void main()
{

	//	Initialze state to indicate no wx samples were taken
	wxInitialSampleTaken = 0;
   //	Init variable that indicates the last minute that was used to update wx
   nLastMinute 			= -1;
   //	Index into weather sample area
   wxSampleIndex			=	0;


   brdInit();              // Initialize I/O Properties
   variableInit();         // Initialize variable values
   calcData();					// Calculate Initial Data

   //	Determine weather station availability
   nWxStationAvailable = isWeatherStationAvailable();

   //	Display choice for user
   display(0);

	while(1)
   {

  		costate
    	{
         //	Get user input
        	getInput();
    	}

		costate
      {
      	if ( nWxStationAvailable == 1 )
        	{
        		getWeatherLoop();
         }
      }

      costate
      {
      	seedingShutdownCheck();
      }


    }



}


/// <summary>
/// Main weather loop to used to poll for weather information
/// </summary>

void getWeatherLoop()
{

	struct tm t;



   //	Get the current time
   mktm(&t,SEC_TIMER );
   //	See if the current time ends on a 10 minute interval
   //	Note if you change this be sure to update the following or underflow could result
	//	WX_SAMPLING_INTERVAL_MINUTES	10
	//	WX_MAX_SAMPLE_HOURS				8
	//	WX_MAX_SAMPLES					   (60/WX_SAMPLING_INTERVAL_MINUTES)*WX_MAX_SAMPLE_HOURS
   //	Note that although we will store readings every 10 minutes in history
   //	we will get readings every 5 minutes for the get wx now command

   //	If we're evenly divisible by the sampling interval we need to take a sample
   //	or if we haven't taken a sample yet we need to take one immediately
      //if ( nLastMinute != t.tm_min ||  wxInitialSampleTaken ==0   )

  //	printf("\nDebug Minutes: %d  Remainder: %d\n", t.tm_min, t.tm_min % WX_HISTORY_INTERVAL_MINUTES );

   if ( (t.tm_min % WX_SAMPLING_INTERVAL_MINUTES == 0 && nLastMinute != t.tm_min) ||
    	  wxInitialSampleTaken ==0
      )
   {

	   printf("\nTake Sample Max: %d, Index: %d\n",WX_MAX_SAMPLES,wxSampleIndex  );
	   printf("\nSample Minutes: %d  Remainder: %d\n", t.tm_min, t.tm_min % WX_HISTORY_INTERVAL_MINUTES );

      wxInitialSampleTaken	= 1;
   	nLastMinute 			= t.tm_min;
      getWeatherStatus();
      printf(szCurrentWeatherString);

         //	IF were evenly divisible by the history interval we need to store the sample
   	//if ( fmod((float)t.tm_min,(float)WX_HISTORY_INTERVAL_MINUTES) == 0 )

      //	Only store weather string if it is valid (available)
      if (strcmp(szCurrentWeatherString,ERROR_WX_NOT_AVAIL) != 0  &&
          t.tm_min % WX_HISTORY_INTERVAL_MINUTES == 0
         )
   	{
           // Store in the array
      	  strcpy(&wxStorageArea[wxSampleIndex++][0],szCurrentWeatherString);

           if ( wxSampleIndex >= WX_MAX_SAMPLES )
      	  {
           		// If the array is full roll over and start overwriting old entries
           		wxSampleIndex=0;
           }

           printf("\nStore WX sample\n");
      }
   }


}

/// <summary>
/// Function used to send available weather history to client
/// </summary>

void sendWeatherHistory()
{

   int nIndex;
   int nValidString;
   nValidString = 0;




     if ( wxStorageArea[0][0] == 'W' )
     {

      // Send weather command
	   serCputs("h");

	   for ( nIndex=0; nIndex<WX_MAX_SAMPLES;nIndex++ )
	   {
	      if ( wxStorageArea[nIndex][0] != NULL )
	      {

         	//	Indicate we found at least one valid string
         	nValidString = 1;
	         // Add separator character for each weather string
	        if ( nIndex != 0 )
	        {
	         serCputs("|");
	        }
	        serCputs(&wxStorageArea[nIndex][0]);
	      }
	   }
	}
   else
	{
        // Send error message indcating that weather is not available
        serCputs(ERROR_WX_NOT_AVAIL);
   }

   serCputs("\r");
   //display(1);

}

void groundGenControl()
{



   //do
   //{
		switch (choice)
  		{
  			case '1' : {burner(); 						break;}     //Manual Burner 	ON/OFF
   		case '2' : {nozzle(); 						break;}		//Manual Nozzle 	ON/OFF
   		case '3' : {solution();						break;}		//Manual Solution	ON/OFF
   		case '4' : {purge();				  			break;}   	//Manual Purge Lines
   		case '5' : {ignite();						break;}		//Manual Ignite (15sec)
   		case '6' : {closeAll();						break;}		//Close all Solenoids
     		case '7' : {sensor();						break;}		//Sensor Status
  			case 'a' : {seedStart();					break;}		//Seeding Sequence (Start)
  			case 'b' : {seedStop();						break;}		//Seeding Sequence (Stop)
         case 'w' : {getCurrentWeatherString();	break;}     //Weather Station Status
         case 'h' : {sendWeatherHistory(); 		break;}		//Weather station history
		}

      display(0);

   //}
  // while (1);
}
/******************************************************************************/
/********************************Set Variable values***************************/
void variableInit()
{
   // RS-232 Comm Initialization
   serCopen(9600);
   serMode(1);            	       	//initialize both Serial ports C, D
   serCwrFlush();                 	//Clears Serial Write Buffer
	serCrdFlush();                 	//Clears Serial Read Buffer
   serDopen(9600);
   serDwrFlush();                 	//Clears Serial Write Buffer
	serDrdFlush();                 	//Clears Serial Read Buffer



   // END RS-232 Comm Initialization

   statusS1 = 0;   			//Solenoid 1 Status (Burner	 	ON/OFF)
	statusS2 = 0;           //Solenoid 2 Status (Nozzle 		ON/OFF)
	statusS5 = 0;           //Solenoid 5 Stat-us(Solution		ON/OFF)
   statusPurge = 0;        //Purge status 	  (Purge			ON/OFF)
   statusIgnite = 0;       //Igniter status	  (Igniter		ON/OFF)
   seedStatus = 0;         //Seed Status		  (Seeding		ON/OFF)
   tempSensor = 0;         //Temperature Sensors
   flameTemp = 0;          //Flame Temperature
   flameThreshold = 150.0; //Minimum Temperature to Confirm Flame Exists
   battVolts = 0;          //Battery Voltage
   battLowVolts = 0;       //Low Battery Voltage Indicator

   //	Initialize WX storage area
   memset(wxStorageArea,0,sizeof(wxStorageArea));

}

//	Check to see if we can shut down seeding
void seedingShutdownCheck()
{

    	calcData();  //Update parameters for safety checking

      if ( SEC_TIMER >= (lRefseconds + SHUTDOWN_SEEDING_INTERVAL) && seedStatus == 1)
      {
      	printf("Shutdown seeding due to safety timeout...\n");
         purge();   			//Purge Lines
      	closeAll();       //Shutdown
      	//display(1);
      }
      else if (flameTemp <= 75.0 && seedStatus == 1)
      {
   		printf("***Flame-Out!  Threshold Temp Too Low - Shutting Down!***\n\n");
      	errorMessage(9); 	//Shutdown Activation message
         purge();   			//Purge Lines
      	closeAll();       //Shutdown
      	//display(1);
      }
      else if (battVolts <= 10 && seedStatus == 1)
      {
   		printf("***Threshold Voltage Too Low - Shutting Down!***\n\n");
      	errorMessage(10); //Shutdown Activation message
         purge();   			//Purge Lines
      	closeAll();       //Shutdown
      	//display(1);
      }
      else if (pressureMeter <= 10 && seedStatus == 1)
      {
   		printf("***Threshold Pressure Too Low - Shutting Down!***\n\n");
      	errorMessage(11); //Shutdown Activation message
         purge();   			//Purge Lines
      	closeAll();       //Shutdown
      	//display(1);
      }
}


/******************************************************************************/
/*******************************Get User Input*********************************/
void getInput()
{

	costate
   {

		wfd choice = cof_serCgetc();      //RS232 Console Input


	   //Handle Upper Case Characters
	   if (choice == 'A')
	      choice = 'a';           //Start Seeding Selected
	   else if (choice == 'B')
	      choice = 'b';           //Shutdown Selected
	   else if (choice == 'W')
	      choice = 'w';           //Weather Station Status

	   printf("%c",choice);

   	groundGenControl();
   }

}
/******************************************************************************/
/******************************Diagnostic Display******************************/
void display(int nGetInput)
{
	//Dynamic C Console User Screen (Testing Window)
   printf ( " \x1Bt" );		//clear screen
   printf("\n	  Welcome to GroundGen v2.0!\n\n");
   printf("***************Solenoid Status****************\n");
	if (statusS1 == 1)
      printf("Burner - ON	");
   else
   	printf("Burner - OFF	");
   if (statusS2 == 1)
      printf("Nozzle - ON	");
   else
   	printf("Nozzle - OFF	");
   if (statusS5 == 1)
      printf("Solution - ON \n");
   else
   	printf("Solution - OFF \n");
   printf("----------------------------------------------\n");
   printf("Flame Temp:  %7.2f F", flameTemp);
   printf("	Batt. Volts:  %5.2f V\n", battVolts);
   printf("Flow Rate:  %4.2f GPH", flowMeter);
   printf("	Pressure:  %4.1f PSI\n", pressureMeter);
   printf("**********************************************\n\n");
   printf("	(1)	Burner 		ON/OFF\n");
   printf("	(2)	Nozzle 		ON/OFF\n");
   printf("	(3)	Solution 	ON/OFF\n");
   printf("	(4)	Purge Lines\n");
   printf("	(5)	Igniter ON\n");
   printf("	(6)	Close ALL\n");
   printf("	(7)	Sensor Status\n");
   printf("	(A)	Seeding	- Start Sequence\n");
   printf("	(B)	Seeding	- Stop Sequence\n");
   printf("	(W)	Weather Station Status\n");
   printf("	(H)	Weather Station History\n\n\n\n\n");

   if (  nGetInput )
	{
   	getInput();
   }
}

/******************************************************************************/
/********************************Build String**********************************/
void buildString()
{
   auto char tempBuffer[5], battBuffer[5], flowBuffer[5], pressureBuffer[5];

/////////////////////////Solenoid Status String Append//////////////////////////
   commString[0] = 'S';
   if (statusS1 == 1)
      commString[1]='1';
   else
      commString[1]='0';
   if (statusS2 == 1)
      commString[2]='1';
   else
   	commString[2]='0';
	if (statusS5 == 1)
      commString[3]='1';
   else
   	commString[3]='0';
   if (tempSensor == 1 || flameTemp > 150.00)
      commString[4]='1';
   else
      commString[4]='0';

   /////////////////////////Flame Temp String Append////////////////////////////
   itoa(flameTemp*10,tempBuffer);
  	if(flameTemp >=100)
   {
   	commString[5] = tempBuffer[0];
   	commString[6] = tempBuffer[1];
   	commString[7] = tempBuffer[2];
      commString[8] = tempBuffer[3];
   }
   else if (flameTemp >=10 && flameTemp < 100)
   {
   	commString[5] = '0';
   	commString[6] = tempBuffer[0];
   	commString[7] = tempBuffer[1];
   	commString[8] = tempBuffer[2];
   }
   else if (flameTemp >=1 && flameTemp < 10)
   {
   	commString[5] = '0';
   	commString[6] = '0';
   	commString[7] = tempBuffer[0];
   	commString[8] = tempBuffer[1];
   }
   else
   {
   	commString[5] = '0';
   	commString[6] = '0';
   	commString[7] = '0';
   	commString[8] = tempBuffer[0];
   }
   commString[9]= 'F';

   /////////////////////////Batt. Voltage String Append/////////////////////////
   itoa(battVolts*10, battBuffer);
   if(battVolts >= 10)
   {
     	commString[10] = battBuffer[0];
     	commString[11] = battBuffer[1];
      commString[12] = battBuffer[2];
   }
   else
   {
     	commString[10] 	= '0';
      commString[11] = battBuffer[0];
      commString[12] = battBuffer[1];
   }
   commString[13] = 'V';

   ////////////////////////////Flow Meter String Append/////////////////////////
   itoa(flowMeter*100, flowBuffer);
   if (flowMeter >= .1)
   {
   	commString[14] = flowBuffer[0];
   	commString[15] = flowBuffer[1];
   }
   else
   {
   	commString[14] = '0';
   	commString[15] = flowBuffer[0];
   }
   commString[16] = 'G';
   /////////////////////////Purge/Igniter Status String Append//////////////////
   if (statusPurge == 1)
      commString[17]='1';         //
   else                           //Purge Status
      commString[17]='0';         //
   if (statusIgnite == 1)
      commString[18]='1';         //
   else                           //Igniter Status
      commString[18]='0';         //
   if (seedStatus == 1)
      commString[19]='1';         //
   else                           //Seeding Status
      commString[19]='0';         //

   ////////////////////////////////DATE/TIME APPEND/////////////////////////////

  	tm_rd(&rtc);				// get time in struct tm
  	itoa( rtc.tm_mon, month );
   itoa( rtc.tm_mday, day );
   itoa( 1900+rtc.tm_year, year );
   itoa( rtc.tm_hour, hour );
   itoa( rtc.tm_min, min );
   itoa( rtc.tm_sec, sec );

   if(month[1] != '\0')
   {
		commString[20] = month[0];
   	commString[21] = month[1];
   }
   else
   {
		commString[20] = '0';
   	commString[21] = month[0];
   }
   if (day[1] != '\0')
   {
   	commString[22] = day[0];
   	commString[23] = day[1];
   }
   else
   {
   	commString[22] = '0';
   	commString[23] = day[0];
   }

   commString[24] = year[0];
   commString[25] = year[1];
   commString[26] = year[2];
   commString[27] = year[3];

   if (hour[1] != '\0')
   {
   	commString[28] = hour[0];
   	commString[29] = hour[1];
   }
   else
   {
   	commString[28] = '0';
   	commString[29] = hour[0];
   }
   if (min[1] != '\0')
   {
   	commString[30] = min[0];
   	commString[31] = min[1];
   }
   else
   {
   	commString[30] = '0';
   	commString[31] = min[0];
   }
   if (sec[1] != '\0')
   {
   	commString[32] = sec[0];
   	commString[33] = sec[1];
   }
   else
   {
   	commString[32] = '0';
   	commString[33] = sec[0];
   }
   commString[34] = 'T';

   ////////////////////////////FIRMWARE VERSION APPEND//////////////////////////

   commString[35] = codeVersion[0];
   commString[36] = codeVersion[1];
   commString[37] = codeVersion[2];
   commString[38] = codeVersion[3];
   commString[39] = 'F';  // denotes 'Firmware Version'

   ///////////////////////Pressure Meter String Append//////////////////////////
   itoa(pressureMeter*10,pressureBuffer);
  	if(pressureMeter >=10)
   {
   	commString[40] = pressureBuffer[0];
   	commString[41] = pressureBuffer[1];
   	commString[42] = pressureBuffer[2];
   }
   else if (pressureMeter >= 1 && pressureMeter < 10)
   {
   	commString[40] = '0';
   	commString[41] = pressureBuffer[0];
   	commString[42] = pressureBuffer[1];
   }
   else
   {
   	commString[40] = '0';
   	commString[41] = '0';
   	commString[42] = pressureBuffer[0];
   }
   commString[43]= 'P';

   commString[44] = '\0';  //null terminator  END OF STRING
}
/******************************************************************************/
/********************************Flame Temp Calc*******************************/
void calcData()
{
	auto int temp1, temp2, temp3, temp4;
   temp1 = anaIn(ChanAddr(5, 0));  		//Analog Temperature (F)
   temp2 = anaIn(ChanAddr(4, 0));      //Analog Battery Voltage (V)
	temp3 = anaIn(ChanAddr(4, 1));  		//Analog Flow Meter output (GPH)
   temp4 = anaIn(ChanAddr(5, 1));		//Analog Pressure Meter output (PSI)

   flameTemp = (999.9/4096.0)*(4096-temp1);			//Calculated Temp
   battVolts = (14.0/4096)*(4096-temp2);    			//Calculated Batt. volts
   flowMeter = (0.50/4096)*(4096-temp3);				//Calculated Flow Rate
   tempSensor = digIn(ChanAddr(0, 0));       		//Temp Sensor Status
   pressureMeter = (50.0/4096.0)*(4096-temp4);		//Calculated Pressure

}
/******************************************************************************/
/**********************************Status**************************************/
void status()
{
   	calcData();                      //Calculates Data to be retreived
		buildString();                   //Re-builds commString
   	serCwrFlush();                 	//Clears Serial Write Buffer
  		serCrdFlush();                 	//Clears Serial Read Buffer
   	serCputs(commString);
   	serCputs("\r");
}
/******************************************************************************/
/*****************************Burner ON/OFF************************************/
void burner()
{
  	if (statusS1 == 0)
   {
      relayOut(ChanAddr(2, 0),1);				//
     	printf("Burner ON!\n\n");      			//Dynamic C Console Output
      sdelay(1);                             //Burner ON
  		relayOut(ChanAddr(2, 0),0);				//
      statusS1 = 1;
   }
   else if (statusS1 == 1 && choice != '1')
   {
      relayOut(ChanAddr(2, 1),1);				//
      printf("Burner OFF!\n\n");     			//Dynamic C Console Output
		sdelay(1);                             //Burner OFF
  		relayOut(ChanAddr(2, 1),0);				//
      statusS1 = 0;
   }
   else if (statusS1 == 1 && choice == '1')
   {
   	if (statusS5 == 1)
      {
         printf("Error - Must Purge Before Closing Solenoid!\n");
         errorMessage(7);
         //display(1);
      }
      else
      {
      	relayOut(ChanAddr(2, 1),1);				//
      	printf("Burner OFF!\n\n");     			//Dynamic C Console Output
			sdelay(1);                             //Burner OFF
  			relayOut(ChanAddr(2, 1),0);				//
      	statusS1 = 0;
      }
   }
   if (choice == '1' && statusS5 == 0)
   {
   	status();
   	//display(1);
   }
}
/******************************************************************************/
/*****************************Nozzle ON/OFF************************************/
void nozzle()
{
   if (statusS2 == 0)
   {
      relayOut(ChanAddr(2, 2),1);				//
      printf("Nozzle ON!\n\n");     			//Dynamic C Console Output
      sdelay(1);                    			//Nozzle ON
  		relayOut(ChanAddr(2, 2),0);				//
      statusS2 = 1; 									//Solenoid Status
   }
   else if (statusS2 == 1 && choice != '2')
   {
     	relayOut(ChanAddr(2, 3),1);				//
  		printf("Nozzle OFF!\n\n"); 				//Dynamic C Console Output
  		sdelay(1);                       		//Nozzle OFF
		relayOut(ChanAddr(2, 3),0);  				//
  		statusS2 = 0;   								//Solenoid Status
   }
   else if (statusS2 == 1 && choice == '2')
   {
   	if (statusS5 == 1)
      {
         printf("Error - Must Purge Before Closeing Solenoid!\n");
         errorMessage(7);
         //display(1);
      }
      else
      {
     		relayOut(ChanAddr(2, 3),1);				//
  			printf("Nozzle OFF!\n\n"); 				//Dynamic C Console Output
  			sdelay(1);                       		//Nozzle OFF
			relayOut(ChanAddr(2, 3),0);  				//
  			statusS2 = 0;   								//Solenoid Status
      }
   }
   if (choice == '2' && statusS5 == 0)
   {
   	status();
   	//display(1);
   }
}
/******************************************************************************/
/*****************************Solution ON/OFF**********************************/
void solution()
{
   calcData();
   if (tempSensor == 0 && flameTemp < flameThreshold && statusS5 == 0)   //Checks temps sensor for flame
   {
   	printf("Flame Not Lit!");
      errorMessage(1);              //Flame Not lit!
      //display(1);
   }
   else if (statusS5 == 0 && (statusS1 == 0 || statusS2 == 0))   //Checks Burner Solenoid status for open
   {
   	printf("Error - Burner and/or Nozzle Not Open, No Flame!");
      errorMessage(6);              //Flame Not lit!
      //display(1);
   }
   else
   {
   	if (statusS5 == 0)
   	{
      	printf("Solution ON!\n\n");   //Dynamic C Console Output
      	relayOut(ChanAddr(2, 7),1);	//
      	relayOut(ChanAddr(2, 5),1);	//
      	sdelay(1);                    // Safety Check - Purge Closed!
      	relayOut(ChanAddr(2, 7),0);   //
      	relayOut(ChanAddr(2, 5),0);	//
      	relayOut(ChanAddr(1, 0),1);	//
			sdelay(1);                    // SOLUTION ON
  			relayOut(ChanAddr(1, 0),0);	//
   		statusS5 = 1;
      	if (statusS1 == 1 && statusS2 == 1 && tempSensor == 1)
      		seedStatus = 1;        //Requirments for manual seeding are met
   	}
      else
   	{
      	if (choice == '3' && statusS5 == 1)   //Checks Burner Solenoid status for open
   		{
   			printf("Error - Must Purge Lines Before Closing Solenoid!");
      		errorMessage(7);              //Must Purge Lines First
      		//display(1);
   		}
         else
         {
         	printf("Solution OFF!\n\n");   //Dynamic C Console Output
      		relayOut(ChanAddr(1, 1),1);	//
				sdelay(1);                   	// SOLUTION OFF
  				relayOut(ChanAddr(1, 1),0);	//
      		statusS5 = 0;
         }
   	}
   	if(choice == '3')
   	{
     		status();
     		//display(1);
   	}
   }
}
/******************************************************************************/
/******************************CLOSE ALL SOLENOIDS********************************/
void closeAll()
{
	auto int i;
	printf("Closing All Solenoids...");

   relayOut(ChanAddr(2, 1),1);	// Close Burner
   relayOut(ChanAddr(2, 3),1);	// Close Nozzle
   relayOut(ChanAddr(2, 5),1);	// Close pPurge
   relayOut(ChanAddr(2, 7),1);	// Close sPurge
   relayOut(ChanAddr(1, 1),1);	// Close Solution
   sdelay(1);                    //
  	relayOut(ChanAddr(2, 1),0);   //
   relayOut(ChanAddr(2, 3),0);   //
   relayOut(ChanAddr(2, 5),0);   //
   relayOut(ChanAddr(2, 7),0);   //
   relayOut(ChanAddr(1, 1),0);   //
   statusS1 = 0;
   statusS2 = 0;
   statusS5 = 0;
   seedStatus = 0;
   statusPurge = 0;
   statusIgnite = 0;
   for (i = 0; i < 15; i++)
   {
   	relayOut(ChanAddr(1, 2),1);	// Igniter ON
   	sdelay(1);
   	relayOut(ChanAddr(1, 2),0);	// Igniter ON
   	printf(".");
   	sdelay(1);
   }
   status();
   //if (choice == '6')
   	//display(1);

}
/******************************************************************************/
/*******************************Ignite (15sec)*********************************/
void ignite()
{
   auto int i;
   if (choice == '5')
   {
      printf("Choice: Ignite! (15 sec)\n\n"); // Ignite Until Flame is Confirmed
      statusIgnite = 1;
      status();
      for (i = 0; i < 15; i++)
      {
      	relayOut(ChanAddr(1, 2),1);	// Igniter ON
         sdelay(1);
         relayOut(ChanAddr(1, 2),0);	// Igniter ON
         printf(".");
     	 	sdelay(1);
      }
      statusIgnite = 0;
      status();
      //display(1);
   }
  else
   {
      i = 0;
      statusIgnite = 1;
      status();
      printf("Igniting");        // Ignite Until Flame is Confirmed
      do
   	{
      	relayOut(ChanAddr(1, 2),1);	// Igniter ON
         sdelay(1);
         relayOut(ChanAddr(1, 2),0);	// Igniter ON
         printf(".");
     	 	sdelay(1);
         calcData();
         i++;
         if (tempSensor != 0 || i >=45)  //Check temp sensors, 1 = flame,
         	break;                       //0 = No Flame, Timeout 3 min
   	}
      while (flameTemp < flameThreshold);
      if (i >= 45)   //timeout - close all solenoid + send error message
      {

         closeAll();       //Shutdown
         errorMessage(2);	//Flame did not light
      }
      else
      {
      	printf("Flame Temp Confirmed!\n\n");   	// Flame Confirmed!
      	relayOut(ChanAddr(1, 3),1);					// Igniter OFF
      	sdelay(1);
      	relayOut(ChanAddr(1, 3),0);					// Igniter OFF
      	printf("Igniter OFF!\n\n");
      	statusIgnite = 0;
      }
   }
}
/******************************************************************************/
/*******************************Purge Lines************************************/
void purge()
{
	// Purge Lines
   auto int i,g;//loop local variable

	seedStatus = 0;
   statusS2=1;     //set to 1 so they shut off
   statusS5=1;
   nozzle();
   solution();     //must have this to close solenoids

   if (choice == '4')
   {
      statusS1 = 0;   //MUST BE = 0;
      burner();							//Turn Burner on
      printf("Igniting");
      statusPurge = 1;
      status();

      for (g=0; g<10; g++) 			//IGNITION LOOP
      {
         relayOut(ChanAddr(1, 2),1);   //
         sdelay(1);                    // Igniter ON (10 sec)
         printf(".");
         relayOut(ChanAddr(1, 2),0);   //
         sdelay(1);
      }

      relayOut(ChanAddr(2, 4),1);	//
     	relayOut(ChanAddr(2, 6),1);	//Opening Purge Solenoids
    	sdelay(1);                    //
  	  	relayOut(ChanAddr(2, 4),0);	//
     	relayOut(ChanAddr(2, 6),0);	//
      printf("\nPurging Lines");
      for (i = 0; i <= 6; i++)   			//PURGE LOOP
   	{
         printf(".");            		//Set Purge Loop Time (i) in Seconds
   		sdelay(10);              		//PURGE FOR 60 SEC / 1 min (i=6)
   	}
      statusPurge = 0;						//Purge Complete
      seedStatus = 0;
      burner();   							//Turn Burner OFF

      for (i = 0; i < 15; i++) //Continue to ignite to ensure flame is lit to burn
      {                        //any excess chemical
      	relayOut(ChanAddr(1, 2),1);	// Igniter ON
         sdelay(1);
         relayOut(ChanAddr(1, 2),0);	// Igniter ON
         printf(".");
     	 	sdelay(1);
      }

      status();
      //display(1);
   }
   else
   {
   	relayOut(ChanAddr(2, 4),1);	//
   	relayOut(ChanAddr(2, 6),1);	//Opening Purge Solenoids
   	sdelay(1);                    //
  		relayOut(ChanAddr(2, 4),0);	//
   	relayOut(ChanAddr(2, 6),0);	//
      printf("Purging Lines");      //Dynamic C Console Output
      statusPurge = 1;
      seedStatus = 0;
      status();
      for (i = 0; i < 6; i++)     	//Loop i <=6 = 1min
   	{
   	 	printf(".");               //Dynamic C Console Output
   	 	sdelay(10);						// PURGE FOR 6(10) SEC
   	}
      relayOut(ChanAddr(2, 5),1);      //
   	relayOut(ChanAddr(2, 7),1);      // Close sPurge Solenoid
   	sdelay(1);                       // Close pPurge Solenoid
   	relayOut(ChanAddr(2, 5),0);      //
   	relayOut(ChanAddr(2, 7),0);      //
   	printf("Complete!\n\n");         //Dynamic C Console Output
   	statusPurge = 0;                 //Purge Complete
   }
}
/******************************************************************************/
/*******************************Sensor Status**********************************/
void sensor()
{
	status();
	//display(1);
}
/******************************************************************************/
/*********************************START SEEDING********************************/
void seedStart()
{
	// TURN ON PROPANE
   if(seedStatus == 1 && statusS1 == 1 && statusS2 == 1 && statusS5 == 1)
   {
   	printf("\nAlready Seeding!\n");
      errorMessage(3);
      //display(1);
   }
   else
   {
   	printf("***Seeding Sequence Initiated!***\n\n\n");
      tempSensorCheck = digIn(ChanAddr(0, 0));
   	if (tempSensorCheck == 0)
   	{
         if (statusS1 == 0)     // Burner not open yet
         {
         	printf("Turning Burner ON...");
      		burner();  //Turn Burner ON
            ignite();   // IGNITION
         	if (statusS1 == 1)
         	{
               if (statusS2 == 0)
               {
               	printf("Turning Nozzle ON...");
                  nozzle();
   					// TURN ON SOLUTION
   					printf("Close Purge Solenoids...");
   					solution();
      				seedStatus = 1;
      				status();
            		serCputs("IOK\r");     //OK message, not display ed to User
               }
               else
               {
               	// TURN ON SOLUTION
   					printf("Close Purge Solenoids...");
   					solution();
      				seedStatus = 1;
      				status();
            		serCputs("IOK\r");     //OK message, not displayed to User
               }
         	}
         }
         else
         {
      		ignite();   // IGNITION
         	if (statusS1 == 1)
         	{	;
               if (statusS2 == 0)
               {
               	printf("Turning Nozzle ON...");
                  nozzle();
   					// TURN ON SOLUTION
   					printf("Close Purge Solenoids...");
   					solution();
      				seedStatus = 1;
      				status();
            		serCputs("IOK\r");     //OK message, not displayed to User
               }
               else
               {
               	// TURN ON SOLUTION
   					printf("Close Purge Solenoids...");
   					solution();
      				seedStatus = 1;
      				status();
            		serCputs("IOK\r");     //OK message, not displayed to User
               }
         	}
         }
         lRefseconds = (unsigned long int) SEC_TIMER;
      }
   	else
   	{
   		printf("***CHECK SENSOR CONNECTION!!***\n\n");
			errorMessage(5);
      	closeAll;

      }
      //display(1);
  	}
}
/******************************************************************************/
/*********************************STOP SEEDING*********************************/
void seedStop()
{
	// Purge Lines
   auto int i;	//loop counter local variable
   if(seedStatus == 0)
   {
   	printf("\nSeeding Never Initiated!\n");
      errorMessage(4);
      //display(1);
   }
   else
   {
   	printf("***Shutdown Sequence Initiated!***\n\n");
      purge();   			//Purge Lines
      closeAll();       //Shutdown
      //display(1);
   }
}
/******************************************************************************/
/*******************************Error Handler**********************************/
void errorMessage(int x)
{
   if (x == 1)
   {serCputs("Error - Flame Not Present!\r");}
   else if (x == 2)
	{serCputs("Error - Failed to Ignite Flame!\r");}
   else if (x == 3)
	{serCputs("Error - Seeding Already Initialized!\r");}
   else if (x == 4)
	{serCputs("Error - Seeding Never Initialized!\r");}
   else if (x == 5)
	{serCputs("Error - Check Temp Sensor Connection!\r");}
   else if (x == 6)
	{serCputs("Error - Burner and/or Nozzle Solenoid Not Open, No Flame!\r");}
   else if (x == 7)
   {serCputs("Error - Must Purge Lines before Closing Solenoid!\r");}
   else if (x == 8)
   {serCputs("Error - Weather Station Information Not Available!\r");}
   else if (x == 9)
   {serCputs("Error - Flame-out! Flame Temp Too Low! - Shutting Down!\r");}
   else if (x == 10)
   {serCputs("Error - Batt. Voltage Too Low! - Shutting Down!\r");}
   else if (x == 11)
   {serCputs("Error - Tank Pressure Too Low! - Shutting Down!\r");}
}
/******************************************************************************/
/*******************************Wait Delay*************************************/
void sdelay(long sd)
{
	auto unsigned long t1;
	t1 = MS_TIMER + sd*1000;            //sd*1 Second Delay
	while ((long)(MS_TIMER-t1) < 0);
   //costate
  // {
   //	waitfor(DelaySec(sd));
   //}

}
/******************************************************************************/





//////////@@@@@@@ ALL WEATHER STATION METHODS AFTER THIS POINT @@@@@@@//////////


/// <summary>
/// Used to return a weather string indicating that weather data is not avaialable.
///
///*********************************IMPORTANT NOTICE******************************
/// Wx Station MUST be configured to the following specification using the Vaisala
/// field programming cable and software:
/// String 0R1 (contains Wind Params) must be set to 2 second intervals
/// String 0R2 (contains Rel. Humidity, Pressure, etc.) must be set to 5 second intervals.
/// The Wx station will not communicate properly if the intervals are set smaller than
/// the values above. (3-8-07)
///*******************************************************************************
///
/// </summary>

int isWeatherStationAvailable()
{

   auto int x;
   serDwrFlush();                 	//Clears Serial Write Buffer
  	serDrdFlush();                 	//Clears Serial Read Buffer

   sdelay(6); 			//Allows 6 sec for buffer if Weather Station available
   						//Weather Station sends out every (5 sec)
   x = serDpeek();  	//Check that Weather Station is available!

   if (x == -1)
   {
      storeWXNotAvailable();
      printf("Error - Weather Station Information Not Available!\n");
      return 0;
   }

   return 1;

}

////////////////////////////////Get Weather Data////////////////////////////////
void getWeatherStatus()
{

    auto int x;
    int wxOffset;
    int nRead;
    unsigned long lCharTimer;



   serDwrFlush();                 	//Clears Serial Write Buffer
  	serDrdFlush();                 	//Clears Serial Read Buffer
   flag1 = 0;
   flag2 = 0;
   flag3 = 0;
   whileFlag = 0;


   	while (whileFlag == 0)
  		{
		  costate
        {

        		// Ensure input buffer is clear so we only read recent strings
            serDrdFlush();

      		//	Read character string and get size of command
           memset ( weatherString, 0, sizeof(weatherString) );

           wfd getOk = cof_serDgets (weatherString, sizeof(weatherString)-1, 50 );

          	if (!getOk)
         	{
	        			//	Incomplete string, wait for next input
            		continue;
         	}

         	printf(weatherString);
            printf("\n");
            parseWeatherString();
         	if (flag1 && flag2 && flag3)  //all weather parameters updated
         	{
         		//buildWeatherString(); //will return fully updated weather
                                  	 //string after 30sec has passed.

            	storeCurrentWeatherString();

            	flag1 = 0; 		//reset Wind speed and direction update flag
            	flag2 = 0;     //reset Air Temp, Pressure, Humidity update flag
            	flag3 = 0;     //reset Heater Status update flag
            	whileFlag = 1;
         	}
       }
  		}
  		whileFlag = 0;
   	//display1();



}
////////////////////////////////////////////////////////////////////////////////
///////////////////////////Parse Weather String/////////////////////////////////

 void parseWeatherString()
{
	auto int x, y,
   			directionSize, directionStart,
            speedSize, 		speedStart,
            tempSize, 		tempStart,
            humiditySize, 	humidityStart,
            pressureSize, 	pressureStart,
            heaterSize, 	heaterStart;

	stringType = weatherString[2];
   if (stringType == '1' && flag1 !=1)
  	{
   	//Gather information about parameters in weatherString
      startDirection = strstr(weatherString,"Dm=");  		//search for string
      endDirection = strstr(weatherString,"D,Dx=");  		//ending position
      startSpeed = strstr(weatherString,"Sm=");          //search for string
      endSpeed = strstr(weatherString,"M,Sx=");          //ending position

      if (startDirection == 0 || startSpeed == 0)
   	{
           return;
      }

      //Testing the string for valid parameters, waits for string to come up
      //again if a parameter doesn't exist during this pass

      ///////////Pars average wind direction from weatherString////////////

      //	Clear average wind direction string
     	directionSize = endDirection - startDirection-3; 	//size of substring
    	directionStart = startDirection-weatherString+3;   //starting position
      memset(avewindDirection,0,sizeof(avewindDirection));
      memcpy(avewindDirection, weatherString+directionStart, directionSize );
      printf(avewindDirection);

      printf("\n");

      ///////////Parse wind speed from weatherString///////////////
      startSpeed = strstr(weatherString,"Sm=");          //search for string
      endSpeed = strstr(weatherString,"M,Sx=");          //ending position
      speedSize = endSpeed - startSpeed - 3;     			//size of substring
      speedStart = startSpeed-weatherString+3;           //starting position

  	   memset(avewindSpeed,0,sizeof(avewindSpeed));
      memcpy(avewindSpeed, weatherString+speedStart, speedSize );
      printf(avewindSpeed);
 		printf("\n");

      ///////////////////////////////////////////////////////////////
      flag1 = 1;		//Set flag that information has been updated.
   }

   if (stringType == '2' && flag2 != 1)
   {
      //Gathering information about parameters in weatherString
   	startTemperature = strstr(weatherString,"Ta=");    //search for string
      endTemperature = strchr(weatherString,'C');       	//ending position
      startHumidity = strstr(weatherString,"Ua=");     	//search for string
      endHumidity = strchr(weatherString,'P');         	//ending position
      startPressure = strstr(weatherString,"Pa=");      	//search for string
      endPressure = strchr(weatherString,'M');          	//ending position

      if (startTemperature == 0 || startHumidity == 0 || startPressure == 0)
      { 	//Testing the string for valid parameters, waits for string to come up
      	//again if a parameter doesn't exist during this pass
         return;
      }

      	/////////Parse Air Temperature from weatherString///////////

      	tempSize = endTemperature - startTemperature-3;   	//size of substring
     	 	tempStart = startTemperature-weatherString+3;     	//starting position

  	   	memset(airTemp,0,sizeof(airTemp));
      	memcpy(airTemp, weatherString+tempStart, tempSize );
      	printf(airTemp);
 			printf("\n");


			////////Parse Relative Humidity from weatherString/////////

      	humiditySize = endHumidity - startHumidity-3;    	//size of substring
      	humidityStart = startHumidity-weatherString+3;   	//starting position
 	   	memset(relHumidity,0,sizeof(relHumidity));
      	memcpy(relHumidity, weatherString+humidityStart, humiditySize );
      	printf(relHumidity);
 			printf("\n");


      	//////////Parse Air Pressure from weatherString///////////

      	pressureSize = endPressure - startPressure-3;    	//size of substring
      	pressureStart = startPressure-weatherString+3;    	//starting position
  	   	memset(airPressure,0,sizeof(airPressure));
      	memcpy(airPressure, weatherString+pressureStart, pressureSize );
      	printf(airPressure);
 			printf("\n");


      	////////////////////////////////////////////////////////////

      	flag2 = 1;     //Set flag that information has been updated.

   }

   if (stringType == '5' && flag3 !=1)
   {
   	////////Parse Heater Status from weatherString////////////

   	endHeater = strstr(weatherString,",Vs");
      if (endHeater == 0)
      {
           return;
      }

		heaterStatus[0] = weatherString[endHeater-weatherString-1];
     	heaterStatus[1] = '\0';
     	printf(heaterStatus);
     	printf("\n");


      ///////////////////////////////////////////////////////////

      flag3 = 1;		//Set flag that information has been updated.
   }
}
/*
void parseWeatherString()
{
	auto int x, y,
   			directionSize, directionStart,
            speedSize, 		speedStart,
            tempSize, 		tempStart,
            humiditySize, 	humidityStart,
            pressureSize, 	pressureStart,
            heaterSize, 	heaterStart;

	stringType = weatherString[2];
   if (stringType == '1' && flag1 !=1)
  	{
   	//Gather information about parameters in weatherString
      startDirection = strstr(weatherString,"Dm=");  		//search for string
      endDirection = strstr(weatherString,"D,Dx=");  		//ending position
      startSpeed = strstr(weatherString,"Sm=");          //search for string
      endSpeed = strstr(weatherString,"M,Sx=");          //ending position

      if (startDirection != 0 && startSpeed != 0)
   	{  //Testing the string for valid parameters, waits for string to come up
      	//again if a parameter doesn't exist during this pass

      	///////////Parsing wind direction from weatherString////////////

      	directionSize = endDirection - startDirection-3; 	//size of substring
      	directionStart = startDirection-weatherString+3;   //starting position
      	for (x = 0; x <= directionSize; x++)  //Grap all characters in substring
         	avewindDirection[x] = weatherString[directionStart + x];
      	y = 9-directionSize;
      	for (x = 0; x <= y; x++) //set remaining characters to null
      		avewindDirection[9-x] = '\0';
      	printf(avewindDirection);
      	printf("\n");

      	///////////Parsing wind speed from weatherString///////////////

      	startSpeed = strstr(weatherString,"Sm=");          //search for string
      	endSpeed = strstr(weatherString,"M,Sx=");          //ending position
      	speedSize = endSpeed - startSpeed - 3;     			//size of substring
      	speedStart = startSpeed-weatherString+3;           //starting position
      	for (x = 0; x <= speedSize; x++) //Grap all characters in substring
      		avewindSpeed[x] = weatherString[speedStart + x];
      	y = 9-speedSize;
      	for (x = 0; x <= y; x++) //set remaining characters to null
      		avewindSpeed[9-x] = '\0';
      	printf(avewindSpeed);
 			printf("\n");
      	///////////////////////////////////////////////////////////////

      	flag1 = 1;		//Set flag that information has been updated.
      }
      else
      	return;
   }

   if (stringType == '2' && flag2 != 1)
   {
      //Gathering information about parameters in weatherString
   	startTemperature = strstr(weatherString,"Ta=");    //search for string
      endTemperature = strchr(weatherString,'C');       	//ending position
      startHumidity = strstr(weatherString,"Ua=");     	//search for string
      endHumidity = strchr(weatherString,'P');         	//ending position
      startPressure = strstr(weatherString,"Pa=");      	//search for string
      endPressure = strchr(weatherString,'M');          	//ending position

      if (startTemperature != 0 && startHumidity != 0 && startPressure != 0)
      { 	//Testing the string for valid parameters, waits for string to come up
      	//again if a parameter doesn't exist during this pass

      	/////////Parsing Air Temperature from weatherString///////////

      	tempSize = endTemperature - startTemperature-3;   	//size of substring
     	 	tempStart = startTemperature-weatherString+3;     	//starting position
      	for (x = 0; x <= tempSize; x++) //Grap all characters in substring
         	airTemp[x] = weatherString[tempStart + x];
      	y = 9-tempSize;
      	for (x = 0; x <= y; x++) //set remaining characters to null
      		airTemp[9-x] = '\0';
      	printf(airTemp);
      	printf("\n");

			////////Parsing Relative Humidity from weatherString/////////

      	humiditySize = endHumidity - startHumidity-3;    	//size of substring
      	humidityStart = startHumidity-weatherString+3;   	//starting position
      	for (x = 0; x <= humiditySize; x++) //Grap all characters in substring
         relHumidity[x] = weatherString[humidityStart + x];
      	y = 9-humiditySize;
      	for (x = 0; x <= y; x++) //set remaining characters to null
      		relHumidity[9-x] = '\0';
      	printf(relHumidity);
      	printf("\n");

      	//////////Parsing Air Pressure from weatherString///////////

      	pressureSize = endPressure - startPressure-3;    	//size of substring
      	pressureStart = startPressure-weatherString+3;    	//starting position
      	for (x = 0; x <= pressureSize; x++) //Grap all characters in substring
         	airPressure[x] = weatherString[pressureStart + x];
      	y = 9-pressureSize;
      	for (x = 0; x <= y; x++) //set remaining characters to null
      		airPressure[9-x] = '\0';
      	printf(airPressure);
      	printf("\n");

      	////////////////////////////////////////////////////////////

      	flag2 = 1;     //Set flag that information has been updated.
      }
      else
      	return;
   }
   if (stringType == '5' && flag3 !=1)
   {
   	////////Parsing Heater Status from weatherString////////////

   	endHeater = strstr(weatherString,",Vs");
      if (endHeater != 0)
      {
			heaterStatus[0] = weatherString[endHeater-weatherString-1];
      	heaterStatus[1] = '\0';
      	printf(heaterStatus);
      	printf("\n");
      }
      else
      	return;

      ///////////////////////////////////////////////////////////

      flag3 = 1;		//Set flag that information has been updated.
   }
}
 */

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////Weather String//////////////////////////////////

/*
void buildWeatherString()  //fully updated weatherString in ~30sec
{
   serCputs("W");    //Place holder - Type of string - W = Weather
   serCputs(avewindDirection);
   serCputs("D");    //Place holder - Measurment Unit - D = Degrees
   serCputs(avewindSpeed);
   serCputs("S");    //Place holder - Measurment Unit - S = Meters/sec
   serCputs(airTemp);
   serCputs("C");    //Place holder - Measurment Unit - C = Celcius
   serCputs(relHumidity);
   serCputs("H");    //Place holder - Measurment Unit - H = Percent (%)
   serCputs(airPressure);
   serCputs("P");    //Place holder - Measurment Unit - P = mmHg
   serCputs(heaterStatus);
   serCputs("\0");	//Remaining characters set to null
   serCputs("\r");   //return line
}
*/


void storeCurrentWeatherString()
{

	//	Get current time for weather string
	struct tm t;
  	mktm(&t, SEC_TIMER);

   //	Build weather string
   memset(szCurrentWeatherString,0,sizeof(szCurrentWeatherString));
   sprintf(szCurrentWeatherString,
           "W%02d%02d%04d%02d%02d%02d%sD%sS%sC%sH%sP%s",
           t.tm_mon,t.tm_mday,t.tm_year + 1900, t.tm_hour,t.tm_min,t.tm_sec,
           avewindDirection,
           avewindSpeed,
           airTemp,
           relHumidity,
           airPressure,
           heaterStatus);

}

/// <summary>
/// Used to return a weather string indicating that weather data is not avaialable.
/// </summary>


void storeWXNotAvailable()
{

	//	Get current time for weather string
	struct tm t;
  	mktm(&t, SEC_TIMER);

   //	Return string indicating that weather data is not available
   memset(szCurrentWeatherString,0,sizeof(szCurrentWeatherString));
   strcpy(szCurrentWeatherString,ERROR_WX_NOT_AVAIL);

}

void getCurrentWeatherString()
{

		 // Send most recent WX string
		 serCputs(szCurrentWeatherString);
   	 serCputs("\0");	//Remaining characters set to null
   	 serCputs("\r");   //return line
       //display(1);
}

////////////////////////////////////////////////////////////////////////////////