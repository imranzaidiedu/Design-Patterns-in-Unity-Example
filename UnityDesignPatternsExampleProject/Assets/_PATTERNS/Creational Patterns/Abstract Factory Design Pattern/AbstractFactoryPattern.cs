﻿using UnityEngine;
using System.Collections;

// allows a family of related objects without specifying the concrete class
// when there are many objects that can be added or changed dynamically
// you can model everything you can image and have those objects interact through common interfaces

// negative: can get very complicated

namespace AbstractFactory
{
	public class AbstractFactoryPattern : MonoBehaviour 
	{

		void OnEnable () 
		{
			Debug.Log ("------------------");
			Debug.Log ("ABSTRACT FACTORY DESIGN PATTERN");
			// Test here:
			EnemyShipBuilding ufoBuilder = new UFOEnemyShipBuilding();
			ufoBuilder.orderShip(ShipType.UFO);
		}
	}





	public enum ShipType 
	{
		UFO
	}




	public abstract class EnemyShipBuilding
	{
		// abstract order form:
		protected abstract EnemyShip MakeEnemyShip(ShipType type);

		public EnemyShip orderShip(ShipType type)
		{
			EnemyShip ship = MakeEnemyShip(type);

			ship.MakeShip();
			ship.DisplayShip();
			ship.FollowHeroShip();
			ship.Shoot();

			return ship;
		}
	}

	public class UFOEnemyShipBuilding : EnemyShipBuilding
	{
		// Make Ship varies per ship type...
		protected override EnemyShip MakeEnemyShip(ShipType type)
		{
			EnemyShip ship = null;

			if(type == ShipType.UFO)
			{
				IEnemyShipFactory factory = new UFOEnemyShipFactory();
				ship = new UFOEnemyShip(factory);
				ship.name = "UFO";
			}

			return ship;
		}
	}





	public interface IEnemyShipFactory
	{
		IESWeapon AddESGun();
		IESEngine AddESEngine();
	}

	public class UFOEnemyShipFactory : IEnemyShipFactory
	{
		// each factory can add different weapons and stuff
		public IESWeapon AddESGun()
		{
			return new ESUFOGun();
		}

		public IESEngine AddESEngine()
		{
			return new ESUFOEngine();
		}
	}





	public abstract class EnemyShip
	{
		public string name;
		protected IESEngine engine;
		protected IESWeapon weapon;

		public abstract void MakeShip();

		public void DisplayShip()
		{
			Debug.Log (name + " is on the screen.");
		}

		public void FollowHeroShip()
		{
			Debug.Log (name + " follows hero ship with " + engine.ToString());
		}

		public void Shoot()
		{
			Debug.Log (name + " shoots and does " + weapon.ToString());
		}

		public string ToString()
		{
			return "The " + name + " has a speed of " + engine.ToString() + " a firepower of " + weapon.ToString();
		}
	}

	public class UFOEnemyShip : EnemyShip
	{
		IEnemyShipFactory factory;

		public UFOEnemyShip(IEnemyShipFactory factory)
		{
			this.factory = factory;
		}

		public override void MakeShip()
		{
			Debug.Log ("Making enemy ship " + name);
			weapon = factory.AddESGun();
			engine = factory.AddESEngine();
		}
	}


	
	// possible Weapons to swap in and out
	public interface IESWeapon
	{
		string ToString();
	}

	public interface IESEngine 
	{
		string ToString();
	}

	public class ESUFOGun : IESWeapon
	{
		public string ToString() 
		{
			return "20 damage";
		}
	}
	public class ESUFOEngine : IESEngine
	{
		public string ToString() 
		{
			return "1000 mph";
		}
	}

}
