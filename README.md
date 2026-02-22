<div align="center">

# 🐕 ShibaFarm: A Tale of Debt and Drudgery
**Unity Core Gameplay & Technical Architecture Showcase**

[![Watch the Gameplay Video](https://img.youtube.com/vi/YOUR_VIDEO_ID/maxresdefault.jpg)](https://www.youtube.com/watch?v=YOUR_VIDEO_ID)
*Click the image above to watch the gameplay showcase*

---

### 📖 Game Concept
**"Inherit the debt, harvest the future."**  
ShibaFarm is a narrative-driven simulation where players take on the role of a Shiba Inu burdened by a massive inherited debt. To regain freedom, you must manage a farm, go fishing, and explore mines to earn enough to meet aggressive monthly repayments. The game reflects the modern-day struggle of debt through high-pressure economic loops and rewarding gameplay mechanics.

[Play on Itch.io (Optional Link)]() | [Portfolio Website]()

</div>

---

### 🛠 Technical Highlights
This project focuses on building a **scalable and decoupled architecture**, ensuring that core systems are modular and easy to maintain.

*   **Event-Driven Architecture:** Used C# Events to link debt, time, and UI modules. This keeps systems completely independent and prevents spaghetti code.
*   **State-Based Crop Management:** Engineered growth cycles using the **State Pattern**, managing complex transitions (Empty → Tilled → Watered → Grown) without the performance overhead of per-frame updates.
*   **Data-Driven Design:** Leveraged **ScriptableObjects** for all items and crops, allowing the team to balance the economy and swap assets instantly without touching any code.
*   **Persistent World State:** Developed a custom **JSON-based Save/Load system** that tracks dynamic world data, including inventory, debt interest, and the growth stage of every tile.
*   **Physics-Based Interaction:** Refined "Game Feel" by implementing procedural item collection and magnet systems using Coroutines and Rigidbody logic.

---

### 🎮 Gameplay Mechanics
- **Farming:** Multi-stage growth system with daily soil drying logic.
- **Economy:** Dynamic debt system where interest accumulates every morning.
- **Inventory:** Full Drag-and-Drop system with Hotbar integration and item stacking.
- **Interactions:** Dialogue system for NPC debt collectors and automatic environment lighting.

---

### ⌨️ Controls
| Action | Keyboard | Controller |
| :--- | :--- | :--- |
| **Move** | `W` `A` `S` `D` | Left Stick / D-Pad |
| **Interact / Harvest** | `E` | South Face Button |
| **Use Tool / Action** | `Mouse Left` | West Face Button |
| **Inventory** | `I` | Start / Select |
| **Select Slot** | `1` - `5` | Bumpers (L1/R1) |

---

### 👥 Team & Collaboration
This project was developed in a collaborative environment:
- **Gameplay Programmer (Me):** Responsible for technical architecture, core systems, and logic integration.
- **Artists & Designers:** Provided 3D assets and world design. I worked closely with them to ensure technical feasibility and polish the overall "Game Feel."

---

<div align="center">
  
**Interested in my work? Let's connect!**  
[GitHub](https://github.com/songwutmee) | [LinkedIn]() | [Email]()

</div>
