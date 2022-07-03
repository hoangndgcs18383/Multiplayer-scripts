using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;
using Photon.Pun;

public class PlayerMultiplayer : Stats
{
    public PhotonView PV;

    public GameObject cam;
    public GameObject other;
    public GameObject root;
    public GameObject weaponNotMySee;
    public GameObject skin;
    public HealthBarUI healthBarUI;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        healthBarUI.SetMaxHealth(maxHealth);
        healthBar.SetMaxHealth(maxHealth);
        maxExp = 100;

        if (!PlayerPrefs.HasKey("HealthSaved"))
        {
            currentHealth = 30;
        }
        else
        {
            currentHealth = PlayerPrefs.GetInt("HealthSaved");
        }
        expBar.SetMaxExp(maxExp);

        if (!PV.IsMine)
        {
            cam.SetActive(false);
            other.SetActive(false);
            GetComponent<PlayerMultiplayer>().enabled = false;
            GetComponent<SUPERCharacterAIO>().enabled = false;
            GetComponent<PlayerCasting>().enabled = false;
            return;
        }

        if (PV.IsMine)
        {
            root.SetActive(false);
        }
    }

    private void Update()
    {
        //if (!PV.IsMine)
        //    return;

        if (PV.IsMine)
        {
            Score();
            ChangeHP();
            healthBarUI.SetHealth(currentHealth);
            GetExp();

            SetText(currentLife, value.lifeValue);
            SetText(currentGold, value.goldValue);
            SetText(currentAmmo, value.ammoValue);
            SetText(currentLevel, value.levelValue);
            SetText(currentGold, value.finalGold);
            SetText(currentPoint, value.pointValue);
            SetText(currentTrueDame, value.trueDameValue);
            SetText(currentDefense, value.defenseValue);
            SetText(currentHP, value.hpValue);
        }
    }

    private void FixedUpdate()
    {
        //if (!PV.IsMine)
        //    return;
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();

        if (item)
        {
            //inventory.AddItem(new Item(item.item), 1);
            //inventory.Save();
            var type = item.item.type;
            var data = item.item.data;
            var name = item.item.name;

            if (!PV.IsMine)
            {
                switch (type)
                {
                    case ItemType.Food:
                        item.gameObject.SetActive(false);
                        break;
                    case ItemType.Healing:
                        item.gameObject.SetActive(false);
                        break;
                    case ItemType.Weapon:
                        item.gameObject.SetActive(false);
                        skin.GetComponent<Animation>().Play("Pistol Idle");
                        weaponNotMySee.SetActive(true);
                        Debug.Log("Picked Up");
                        SetTextDisplayUI(name + "has picked");
                        break;
                    case ItemType.Helmet:
                        break;
                    case ItemType.Shield:
                        break;
                    case ItemType.Boots:
                        break;
                    case ItemType.Default:
                        break;
                    case ItemType.Ammo:
                        item.gameObject.SetActive(false);
                        break;
                    case ItemType.Resource:
                        item.gameObject.SetActive(false);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ItemType.Food:
                        var foodHealth = Random.Range(data.buffs[1].min, data.buffs[1].max);
                        currentHealth += foodHealth;
                        var foodExp = Random.Range(data.buffs[0].min, data.buffs[0].max);
                        currentExp += foodExp;
                        item.GetComponentInParent<AudioSource>().Play();
                        item.gameObject.SetActive(false);
                        SetTextDisplayUI(name + " + " + foodHealth + " HP");
                        break;
                    case ItemType.Healing:
                        var fullHealth = currentHealth = maxHealth;
                        var healingExp = Random.Range(data.buffs[0].min, data.buffs[0].max);
                        currentExp += healingExp;
                        item.GetComponentInParent<AudioSource>().Play();
                        item.gameObject.SetActive(false);
                        SetTextDisplayUI(name + " + " + fullHealth + "HP");
                        break;
                    case ItemType.Weapon:
                        item.gameObject.SetActive(false);
                        item.GetComponentInParent<AudioSource>().Play();
                        SetTextDisplayUI(name);
                        gunInHand.SetActive(true);
                        imgHandGun.gameObject.SetActive(true);
                        break;
                    case ItemType.Helmet:
                        break;
                    case ItemType.Shield:
                        break;
                    case ItemType.Boots:
                        break;
                    case ItemType.Default:
                        break;
                    case ItemType.Ammo:
                        currentAmmo += 60;
                        var ammoExp = Random.Range(data.buffs[0].min, data.buffs[0].max);
                        currentExp += ammoExp;
                        item.GetComponentInParent<AudioSource>().Play();
                        item.gameObject.SetActive(false);
                        SetTextDisplayUI(name + " + " + 60);
                        break;
                    case ItemType.Resource:
                        var collect = Random.Range(data.buffs[2].min, data.buffs[2].max);
                        currentGold += collect;
                        currentTreasure++;
                        var collectExp = Random.Range(data.buffs[0].min, data.buffs[0].max);
                        currentExp += collectExp;
                        item.GetComponentInParent<AudioSource>().Play();
                        item.gameObject.SetActive(false);
                        SetTextDisplayUI(name + " + " + collect);
                        break;
                }
            }
        }
    }

    void TakeDame(int damage)
    {
        PV.RPC("RPC_TakeDame", RpcTarget.All, damage);
    }

    [PunRPC]
    public void RPC_TakeDame(int damegeAmount)
    {
        if (!PV.IsMine) return;

        currentHealth -= damegeAmount;
    }
}
