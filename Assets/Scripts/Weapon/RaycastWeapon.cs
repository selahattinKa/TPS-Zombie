using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
  class Bullet
  {
    public float time;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;
    public TrailRenderer tracer;

  }

  public ActiveWeapon.WeaponSlot weaponSlot;
  public bool isFiring = false;

  public int fireRate = 25;
  public float bulletSpeed = 1000.0f;
  public float bulletDrop = 0.0f;
  public ParticleSystem muzzleFlash;
  public ParticleSystem[] muzzleFlash1;
  public ParticleSystem hitEffect;
  public TrailRenderer traverEffect;
  public Transform raycastOrigin;
  public Transform raycastDestination;
  public string weaponName;
  public WeaponRecoil recoil;
  private float effectDuration = 2f;
  public float damage = 10f;

  public GameObject magazine;

  public int ammoCount;
  public int clipSize;
  
  [SerializeField] private ParticleSystem impactEffect;
  [SerializeField] private ParticleSystem enemyEffect;
  [SerializeField] private ParticleSystem metalEffect;
  [SerializeField] private ParticleSystem woodEffect;
  [SerializeField] private ParticleSystem stoneEffect;
  
  


  private Ray ray;
  private RaycastHit hitInfo;
  private float accumulatedTime;
  private List<Bullet> bullets = new List<Bullet>();
  private float maxLifeTime = 3.0f;

  private void Awake()
  {
    recoil = GetComponent<WeaponRecoil>();
  }

  Vector3 GetPosition(Bullet bullet)
  {
    Vector3 gravity = Vector3.down * bulletDrop;
    return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) +
           (0.5f * gravity * bullet.time * bullet.time);
  }

  Bullet CreateBullet(Vector3 position, Vector3 velocity)
  {
    Bullet bullet = new Bullet();
    bullet.initialPosition = position;
    bullet.initialVelocity = velocity;
    bullet.time = 0.0f;
    bullet.tracer = Instantiate(traverEffect, position, quaternion.identity);
    bullet.tracer.AddPosition(position);
    return bullet;
  }

  public void StartFiring()
  {
    isFiring = true;
    accumulatedTime = 0.0f;
    //FireBullet();
    recoil.Reset();
  }

  public void UpdateFiring(float deltaTime)
  {
    accumulatedTime += deltaTime;
    float fireInterval = 1.0f / fireRate;
    while (accumulatedTime>= 0.0f)
    {
      FireBullet();
      accumulatedTime -= fireInterval;
    }
  }

  public void UpdateBullets(float deltaTime)
  {
    SimulateBullets(deltaTime);
    DestroyBullets();
  }

  

  void SimulateBullets(float deltaTime)
  {
    bullets.ForEach(bullet =>
    {
      Vector3 p0 = GetPosition(bullet);
      bullet.time += deltaTime;
      Vector3 p1 = GetPosition(bullet);
      RaycastSegment(p0, p1, bullet);

    });
  }
  void DestroyBullets()
  {
    bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
  }

  void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
  {
    Vector3 direction = end - start;
    float distance = direction.magnitude;
    ray.origin = start;
    ray.direction = direction;
    if (Physics.Raycast(ray, out hitInfo, distance))
    {
      
      
      // hitEffect.transform.position = hitInfo.point;
      // hitEffect.transform.forward = hitInfo.normal;
      // hitEffect.Emit(1);
      
      if (hitInfo.collider.GetComponent<Enemy>() != null) // Düşmana atış yapıldığında
      {
        ParticleSystem effect = Instantiate(enemyEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration + effectDuration);
      }
      else if (hitInfo.collider.GetComponent<Wood>() != null) // Tahtaya atış yapıldığında
      {
        ParticleSystem effect = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration + effectDuration);
      }
      else if (hitInfo.collider.GetComponent<Metal>() != null) // Kuma atış yapıldığında
      {
        ParticleSystem effect = Instantiate(metalEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration + effectDuration);
      }
      else if (hitInfo.collider.GetComponent<Stone>() != null) // Kuma atış yapıldığında
      {
        ParticleSystem effect = Instantiate(stoneEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration + effectDuration);
      }
      else
      {
        ParticleSystem effect = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration + effectDuration);
      }
      
      var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
      if (rb2d)
      {
        rb2d.AddForceAtPosition(ray.direction*20, hitInfo.point, ForceMode.Impulse);
      }
      var hitBox = hitInfo.collider.GetComponent<HitBox>();
      if (hitBox)
      {
          hitBox.OnRaycastHit(this, ray.direction);      
      }
      
    
      bullet.tracer.transform.position = hitInfo.point;
      bullet.time = maxLifeTime;
    }
    else
    {
      bullet.tracer.transform.position = end;
    }

  }
  private void FireBullet()
  {
    if (ammoCount <= 0)
    {
      return;
    }

    ammoCount--;
    muzzleFlash.Emit(1);

    //Eğer muzleflash silah hareket ederken sağ sola kayarsa bunu aktif et
    // foreach (var particle in muzzleFlash1)
    // {
    //   particle.Emit(1);
    //
    // }

    Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
    var bullet = CreateBullet(raycastOrigin.position, velocity);
    bullets.Add(bullet);

    recoil.GenerateRecoil(weaponName);
  }

  public void StopFiring()
  {
    isFiring = false;
  }
}
