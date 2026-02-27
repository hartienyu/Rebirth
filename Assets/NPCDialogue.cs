using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // 引入文字组件库

public class NPCDialogue : MonoBehaviour
{
    [Header("UI 绑定")]
    public GameObject promptUI;      // “按F对话”的提示文字
    public GameObject dialoguePanel; // 黑色半透明对话框背景
    public TMP_Text dialogueText;    // 面板里显示对话内容的文字

    [Header("对话内容")]
    [TextArea(3, 5)]
    public string npcMessage = "你好，旅行者！欢迎来到这个世界。";

    private bool isPlayerNearby = false;

    void Start()
    {
        // 游戏开始时自动隐藏这些 UI
        if (promptUI != null) promptUI.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // 如果玩家在附近，并且按下了键盘的 F 键
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            // 切换对话框的状态（如果开着就关掉，关着就打开）
            bool isPanelActive = dialoguePanel.activeSelf;
            dialoguePanel.SetActive(!isPanelActive);

            // 打开对话框时隐藏“按F”提示，关闭时重新显示提示
            promptUI.SetActive(isPanelActive);

            // 如果是打开对话框，就填入NPC的话
            if (!isPanelActive && dialogueText != null)
            {
                dialogueText.text = npcMessage;
            }
        }
    }

    // 玩家进入感应圈时触发
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (promptUI != null) promptUI.SetActive(true); // 显示“按F”提示
        }
    }

    // 玩家离开感应圈时触发
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (promptUI != null) promptUI.SetActive(false); // 隐藏提示
            if (dialoguePanel != null) dialoguePanel.SetActive(false); // 强制关闭对话框
        }
    }
}