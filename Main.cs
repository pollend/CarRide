using UnityEngine;
using System.Collections.Generic;
using TrackedRiderUtility;

public class Main : IMod
{
    public string Identifier { get; set; }
	public static AssetBundleManager AssetBundleManager = null;
    public static Configuration Configeration = null;


    private TrackRiderBinder binder;

    public void onEnabled()
    {
        if (Main.AssetBundleManager == null) {

			AssetBundleManager = new AssetBundleManager (this);
		}

        binder = new TrackRiderBinder ("509e93b5d3723a5a215e5c67500c2e28");


        TrackedRide trackedRide = binder.RegisterTrackedRide<TrackedRide> ("Ghost Mansion Ride","CarRide", "Car Ride");
        trackedRide.price = 1200;
        trackedRide.carTypes = new CoasterCarInstantiator[]{ };

        TrackedRide ghostRide = TrackRideHelper.GetTrackedRide ("Ghost Mansion Ride");
        GhostMansionRideMeshGenerator meshGenerator = binder.RegisterMeshGenerator<GhostMansionRideMeshGenerator> (trackedRide);
        TrackRideHelper.PassMeshGeneratorProperties (ghostRide.meshGenerator,trackedRide.meshGenerator);
        meshGenerator.tubeMaterial = ((GhostMansionRideMeshGenerator)ghostRide.meshGenerator).tubeMaterial;

        trackedRide.maxBankingAngle = 7;
        trackedRide.targetVelocity = 7;
        trackedRide.meshGenerator.customColors = new Color[] 
        { 
            new Color(63f / 255f, 46f / 255f, 37f / 255f, 1), 
            new Color(43f / 255f, 35f / 255f, 35f / 255f, 1), 
            new Color(90f / 255f, 90f / 255f, 90f / 255f, 1) 
        };
        trackedRide.canChangeCarRotation = false;
     
        CoasterCarInstantiator mouseCarInstaniator = binder.RegisterCoasterCarInstaniator<CoasterCarInstantiator>(trackedRide, "MouseCarInstaniator", "Mouse Car", 1, 1, 1);
        BaseCar mouseCar = binder.RegisterCar<BaseCar>( Main.AssetBundleManager.MouseCarGo,"MouseCar",.3f,.1f,true,
            new Color[] { 
                new Color(71f / 255, 71f / 255, 71f / 255), 
                new Color(176f / 255, 7f / 255, 7f / 255), 
                new Color(26f / 255, 26f / 255, 26f / 255),
                new Color(31f / 255, 31f / 255, 31f / 255)}
        );
        mouseCar.gameObject.AddComponent<RestraintRotationController>().closedAngles = new Vector3(0,0,120);
        mouseCarInstaniator.carGO = mouseCar.gameObject;


        CoasterCarInstantiator TruckCarInstaniator = binder.RegisterCoasterCarInstaniator<CoasterCarInstantiator> (trackedRide, "TruckCarInstaniator", "Truck Car", 1, 1, 1);
        BaseCar truckCar = binder.RegisterCar<BaseCar>( Main.AssetBundleManager.TruckGo,"TruckCar", .3f,.35f,true,
            new Color[] { 
                new Color(68f / 255, 58f / 255, 50f / 255), 
                new Color(176f / 255, 7f / 255, 7f / 255), 
                new Color(55f / 255, 32f / 255, 12f / 255),
                new Color(61f / 255, 40f / 255, 19f / 255)}

        );
        truckCar.gameObject.AddComponent<RestraintRotationController>().closedAngles = new Vector3(0,0,120);
        TruckCarInstaniator.carGO = truckCar.gameObject;

        CoasterCarInstantiator sportsCarInstaniator = binder.RegisterCoasterCarInstaniator<CoasterCarInstantiator> (trackedRide, "SportsCarInstaniator", "Sports Car", 1, 1, 1);
        BaseCar sportsCar = binder.RegisterCar<BaseCar>( Main.AssetBundleManager.SportsCarGo,"SportsCar", .3f,.35f,true,
            new Color[] { 
                new Color(.949f, .141f, .145f), 
                new Color(.925f, .937f, .231f), 
                new Color(.754f, .754f, .754f),
                new Color(.788f,.788f, .788f)}
        );
        sportsCar.gameObject.AddComponent<RestraintRotationController>().closedAngles = new Vector3(0,0,120);
        sportsCarInstaniator.carGO = sportsCar.gameObject;

        binder.Apply ();

        //deprecatedMappings
        string oldHash = "asdfawujeba8whe9jnimpiasnd";
        GameObjectHelper.RegisterDeprecatedMapping ("car_ride_coaster_GO" + oldHash , trackedRide.name);

        GameObjectHelper.RegisterDeprecatedMapping ("Mouse_Car@CoasterCarInstantiator" +oldHash, mouseCarInstaniator.name);
        GameObjectHelper.RegisterDeprecatedMapping ("Truck_Car@CoasterCarInstantiator" +oldHash, TruckCarInstaniator.name);
        GameObjectHelper.RegisterDeprecatedMapping ("Sports_Car@CoasterCarInstantiator" +oldHash, sportsCarInstaniator.name);
      
        GameObjectHelper.RegisterDeprecatedMapping ( "Mouse_Car_Front" + oldHash, mouseCar.name);
        GameObjectHelper.RegisterDeprecatedMapping ("Truck_Car_Front"+ oldHash, truckCar.name);
        GameObjectHelper.RegisterDeprecatedMapping ("Sports_Car_Front"+ oldHash, sportsCar.name);

       

	}


    public void onDisabled()
    {
        binder.Unload ();
	}

    public string Name
    {
        get { return "Car Ride"; }
    }

    public string Description
    {
        get { return "A gental ride that follows a central guide rail. The cars are self powered and follow the main guide rail."; }
    }


	public string Path { get; set; }

}

