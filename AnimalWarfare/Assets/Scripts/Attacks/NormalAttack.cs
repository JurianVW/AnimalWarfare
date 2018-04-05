using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : AttackBase {
	public override void Attack(Animal currentAnimal, Animal targetAnimal){
		currentAnimal.transform.LookAt(targetAnimal.transform.parent);
        currentAnimal.GetComponent<Animator>().SetBool("Attack", true);
        currentAnimal.GetComponent<Animator>().SetBool("Attack1", true);
        targetAnimal.IncomingDamage(currentAnimal.currentAttackPower);
	}
}
